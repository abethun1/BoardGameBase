﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{

    public static BoardManager Instance { set; get; }
    private bool[,] allowedMoves { set; get; }

    public Characters[,] Characters { set; get; }
    private Characters selectedCharacter;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> characters;
    private List<GameObject> activeCharacters = new List<GameObject>(); 

    static Quaternion playerOrientation = Quaternion.Euler(0, 0, 0);
    static Quaternion enemyOrientation = Quaternion.Euler(0, 180, 0);

    public bool isPLayerTurn;

    public int boardSizeX;
    public int boardSizeY;

    //Character Directions for movement
    private bool movingUp;
    private bool movingDown;
    private bool movingLeft;
    private bool movingRight;
    private Vector3 movingDirection;
    private Vector3 turningDirection;


    private void Start()
    {
        Instance = this;
        isPLayerTurn = true;
        SpawnAllCharacters();
    }
    public void Update()
    {
        UpdateSelection();
        DrawChessBoard();

        if (Input.GetMouseButtonDown(0) && !movingUp && !movingDown && !movingLeft && !movingRight)
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                if (selectedCharacter == null)
                {
                    //Select Chess Piece
                    SelectCharacter(selectionX, selectionY);
                }
                else
                {
                    //move piece
                    startMoveChessPiece(selectionX, selectionY);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (movingLeft || movingRight || movingUp || movingDown)
        {
            print("entered here");

            float tempPosX = selectedCharacter.transform.position.x;
            float tempPosY = selectedCharacter.transform.position.z;
            float tempSelectionX = (float)selectionX + .5f;
            float tempSelectionY = (float)selectionY + .5f;

            Vector3 newPosition = new Vector3(selectedCharacter.transform.position.x, selectedCharacter.transform.position.y, selectedCharacter.transform.position.z);
            Vector3 newDirection = new Vector3(0, 0, 0);

            if (movingLeft)
            {
                if (tempPosX > tempSelectionX)
                {
                    newPosition.x = newPosition.x - 1;
                    newDirection.x = -1;
                }
                else
                {
                    newPosition.x = newPosition.x + 1;
                    newDirection.x = 0;
                    movingLeft = false;
                }

            }

            if (movingRight)
            {
                if (tempPosX < tempSelectionX)
                {
                    newPosition.x = newPosition.x + 1;
                    newDirection.x = 1;
                }
                else
                {
                    newPosition.x = newPosition.x - 1;
                    newDirection.x = 0;
                    movingRight = false;
                }

            }

            if (movingUp)
            {
                if (tempPosY < tempSelectionY)
                {
                    newPosition.z = newPosition.z + 1;
                    newDirection.z = 1;
                }
                else
                {
                    newPosition.z = newPosition.z - 1;
                    newDirection.z = 0;
                    movingUp = false;
                }
            }

            if (movingDown)
            {
                if (tempPosY > tempSelectionY)
                {
                    newPosition.z = newPosition.z - 1;
                    newDirection.z = -1;
                }
                else
                {
                    newPosition.z = newPosition.z + 1;
                    newDirection.z = 0;
                    movingDown = false;
                }
            }

            setMovingDirection(newDirection);
            if (Mathf.Abs(tempSelectionX - tempPosX) > 2 || Mathf.Abs(tempSelectionY - tempPosY) > 2)
            {
                selectedCharacter.GetComponent<Characters>().movement(true, false, true);
                selectedCharacter.transform.position = Vector3.Lerp(selectedCharacter.transform.position, newPosition, 1.5f * Time.deltaTime);
            }
            else
            {
                selectedCharacter.GetComponent<Characters>().movement(true, true, false);
                selectedCharacter.transform.position = Vector3.Lerp(selectedCharacter.transform.position, newPosition, 1f * Time.deltaTime);
            }

            if (movingLeft == false && movingRight == false && movingUp == false && movingDown == false)
            {
                selectedCharacter.GetComponent<Characters>().movement(false, false, false);
                MoveCharacter(selectionX, selectionY);
            }
        }
    }

    private void UpdateSelection()
    {
        if (!Camera.main)
        {
            return;
        }

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("GamePlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    private void DrawChessBoard()
    {
        Vector3 widthLine = Vector3.right * boardSizeX;
        Vector3 heightLine = Vector3.forward * boardSizeY;

        for(int i = 0; i <= boardSizeX; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for(int j = 0; j <= boardSizeY; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }
        }


    }

    private void SpawnCharacters(int index, int x, int y, bool isPlayer)
    {
        if (isPlayer)
        {
            GameObject go = Instantiate(characters[index], GetTileCenter(x,y), playerOrientation) as GameObject;
            go.transform.SetParent(transform);
            Characters[x, y] = go.GetComponent<Characters>();
            Characters[x, y].SetPosition(x, y);
            activeCharacters.Add(go);
        }
        else
        {
            GameObject go = Instantiate(characters[index], GetTileCenter(x, y), enemyOrientation) as GameObject;
            go.transform.SetParent(transform);
            Characters[x, y] = go.GetComponent<Characters>();
            Characters[x, y].SetPosition(x, y);
            activeCharacters.Add(go);
        }

    }
  
    private void SpawnAllCharacters() {

        activeCharacters = new List<GameObject>();
        Characters = new Characters[boardSizeX, boardSizeY];

        //Spawn Player Team
        SpawnCharacters(0, 2, 0, true);
        SpawnCharacters(0, 3, 0, true);
        SpawnCharacters(0, 4, 0, true);
        SpawnCharacters(0, 5, 0, true);

        //Spawn Enemy Team
        SpawnCharacters(1, 2, 7, false);
        SpawnCharacters(1, 3, 7, false);
        SpawnCharacters(1, 4, 7, false);
        SpawnCharacters(1, 5, 7, false);
    }
    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;

        return origin;

    }
   
    private void SelectCharacter(int x, int y)
    {
        if(Characters[x,y] == null)
        {
            return;
        }
        if(Characters[x,y].isPlayer != isPLayerTurn)
        {
            return;
        }

        allowedMoves = Characters[x, y].PossibleMove();
        selectedCharacter = Characters[x, y];
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
    }

    private void MoveCharacter(int x, int y)
    {
        if (allowedMoves[x,y])
        {
            //sets position in the cool array of characters to null for that position
            Characters[selectedCharacter.CurrentX, selectedCharacter.CurrentY] = null;
            selectedCharacter.transform.position = GetTileCenter(x, y);
            selectedCharacter.SetPosition(x, y);
            Characters[x, y] = selectedCharacter;
        }
        BoardHighlights.Instance.HideHighlights();
        selectedCharacter = null;
    }

    public void startMoveChessPiece(int x, int y)
    {
        //Don't attack the piece just move
        if (allowedMoves[x, y] && getCharacter(x, y) == null)
        {
            //Move Left
            movingLeft = true;

            //Move Right
            movingRight = true;

            //Move Up
            movingUp = true;

            //Move Down
            movingDown = true;
        }

        //Attack and Move

        //Don't move the piece just attack

        else
        {
            BoardHighlights.Instance.HideHighlights();
            selectedCharacter = null;
        }
    }

    public Vector3 getMovingDirection()
    {
        return movingDirection;
    }

    private void setMovingDirection(Vector3 d)
    {
        movingDirection = d;
    }

    public Characters getCharacter(int x, int y)
    {
        Characters c = Characters[x, y];
        return c;
    }

    public int getBoardSizeX()
    {
        return boardSizeX;
    }

    public int getBoardSizeY()
    {
        return boardSizeY;
    }

}
