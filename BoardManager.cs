using UnityEngine;
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

        if (Input.GetMouseButtonDown(0))
        {
            if(selectionX >= 0 && selectionY >= 0)
            {
                if(selectedCharacter == null)
                {
                    SelectCharacter(selectionX, selectionY);
                }
                else
                {
                    MoveCharacter(selectionX, selectionY);
                }
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
        SpawnCharacters(11, 2, 7, false);
        SpawnCharacters(11, 3, 7, false);
        SpawnCharacters(11, 4, 7, false);
        SpawnCharacters(11, 5, 7, false);
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

    public int getBoardSizeX()
    {
        return boardSizeX;
    }

    public int getBoardSizeY()
    {
        return boardSizeY;
    }

}
