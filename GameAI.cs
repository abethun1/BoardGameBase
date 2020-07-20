using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAI : MonoBehaviour
{

    public static GameAI Instance;
    public GameObject[] players;
    public bool moveUp;
    public bool moveDown;
    public bool moveLeft;
    public bool moveRight;
    public bool moveUpLeft;
    public bool moveUpRight;
    public bool moveDownLeft;
    public bool moveDownRight;

    // Use this for initialization
    void Start()
    {
        Instance = this;
        players = BoardManager.Instance.getPlayers();
        moveUp = false;
        moveDown = false;
        moveLeft = false;
        moveRight = false;
        moveUpLeft = false;
        moveUpRight = false;
        moveDownLeft = false;
        moveDownRight = false;

}

    public void movePiece()
    {

        // bool[,] moves = BoardManager.Instance.selectedCharacter.PossibleMove();
        resetEnemyDirection();
        setEnemyDirection();
        if (getMoveUp())
        {
            makeSelectionX(BoardManager.Instance.selectedCharacter.CurrentX);
            makeSelectionY(BoardManager.Instance.selectedCharacter.CurrentY + 1);
            Debug.Log("move Up");
        }
        else if (getMoveDown())
        {
            makeSelectionX(BoardManager.Instance.selectedCharacter.CurrentX);
            makeSelectionY(BoardManager.Instance.selectedCharacter.CurrentY - 1);
            Debug.Log("move Down");
        }
        else if (getMoveLeft())
        {
            makeSelectionX(BoardManager.Instance.selectedCharacter.CurrentX - 1);
            makeSelectionY(BoardManager.Instance.selectedCharacter.CurrentY);
            Debug.Log("move Left");
        }
        else if (getMoveRight())
        {
            makeSelectionX(BoardManager.Instance.selectedCharacter.CurrentX + 1);
            makeSelectionY(BoardManager.Instance.selectedCharacter.CurrentY);
            Debug.Log("move Right");
        }
        else if (getMoveUpLeft())
        {
            makeSelectionX(BoardManager.Instance.selectedCharacter.CurrentX - 1);
            makeSelectionY(BoardManager.Instance.selectedCharacter.CurrentY + 1);
            Debug.Log("move UpLeft");
        }
        else if (getMoveUpRight())
        {
            makeSelectionX(BoardManager.Instance.selectedCharacter.CurrentX + 1);
            makeSelectionY(BoardManager.Instance.selectedCharacter.CurrentY + 1);
            Debug.Log("move Up Right");
        }
        else if (getMoveDownLeft())
        {
            makeSelectionX(BoardManager.Instance.selectedCharacter.CurrentX - 1);
            makeSelectionY(BoardManager.Instance.selectedCharacter.CurrentY - 1);
            Debug.Log("move Down Left");
        }
        else if (getMoveDownRight())
        {
            makeSelectionX(BoardManager.Instance.selectedCharacter.CurrentX + 1);
            makeSelectionY(BoardManager.Instance.selectedCharacter.CurrentY - 1);
            Debug.Log("move Down Right");
        }



        /*
        for (int i = 0; i < BoardManager.Instance.getBoardSizeX(); i++)
        {
            for (int j = 0; j < BoardManager.Instance.getBoardSizeY(); j++)
            {
                if (moves[i, j])
                {
                    //determines where the piece goes
                        //moving Chess Piece x to position i , j);

                        return;
                }
            }
        }
        */
    }

    private void setEnemyDirection()
    {
        //Enemy (players) coordinates
        int playerX = players[0].GetComponent<Ami>().CurrentX;
        int playerY = players[0].GetComponent<Ami>().CurrentY;

        int enemyX = BoardManager.Instance.selectedCharacter.CurrentX;
        int enemyY = BoardManager.Instance.selectedCharacter.CurrentY;

        //should move up
        if((enemyX - playerX) == 0 && (enemyY - playerY) < 0)
        {
            setMoveUp(true);
            return;
        }
        //should move down 
        else if((enemyX - playerX) == 0 && (enemyY - playerY) > 0)
        {
            setMoveDown(true);
            return;
        }
        //should move left 
        else if ((enemyX - playerX) > 0 && (enemyY - playerY) == 0)
        {
            setMoveLeft(true);
            return;
        }
        //should move right 
        else if ((enemyX - playerX) < 0 && (enemyY - playerY) == 0)
        {
            setMoveRight(true);
            return;
        }
        //should move upRight
        else if ((enemyX - playerX) < 0 && (enemyY - playerY) < 0)
        {
            setMoveUpRight(true);
            return;
        }
        //should move upLeft
        else if ((enemyX - playerX) > 0 && (enemyY - playerY) < 0)
        {
            setMoveUpLeft(true);
            return;
        }
        //should move downLeft 
        else if ((enemyX - playerX) > 0 && (enemyY - playerY) > 0)
        {
            setMoveDownLeft(true);
            return;
        }
        //should move downRight 
        else if ((enemyX - playerX) < 0 && (enemyY - playerY) > 0)
        {
            setMoveDownRight(true);
            return;
        }
        return;
    }

    private void resetEnemyDirection()
    {
        setMoveDown(false);
        setMoveUp(false);
        setMoveRight(false);
        setMoveLeft(false);
        setMoveUpLeft(false);
        setMoveUpRight(false);
        setMoveDownLeft(false);
        setMoveDownRight(false);
    }


    //Here make a method to calculate the best move for an AI

    public void makeSelectionX(int x)
    {
        BoardManager.Instance.setSelectionX(x);
    }
    public void makeSelectionY(int y)
    {
        BoardManager.Instance.setSelectionY(y);
    }

    //Get directions of player(enemy)

    public bool getMoveUp()
    {
        return moveUp;
    }
    public bool getMoveDown()
    {
        return moveDown;
    }
    public bool getMoveLeft()
    {
        return moveLeft;
    }
    public bool getMoveRight()
    {
        return moveRight;
    }
    public bool getMoveUpRight()
    {
        return moveUpRight;
    }
    public bool getMoveUpLeft()
    {
        return moveUpLeft;
    }
    public bool getMoveDownLeft()
    {
        return moveDownLeft;
    }
    public bool getMoveDownRight()
    {
        return moveDownRight;
    }
    public void setMoveUp(bool s)
    {
        moveUp = s;
    }
    public void setMoveDown(bool s)
    {
        moveDown = s;
    }
    public void setMoveLeft(bool s)
    {
         moveLeft = s;
    }
    public void setMoveRight(bool s)
    {
        moveRight = s;
    }
    public void setMoveUpRight(bool s)
    {
        moveUpRight = s;
    }
    public void setMoveUpLeft(bool s)
    {
        moveUpLeft = s;
    }
    public void setMoveDownLeft(bool s)
    {
        moveDownLeft = s;
    }
    public void setMoveDownRight(bool s)
    {
        moveDownRight = s;
    }

}
