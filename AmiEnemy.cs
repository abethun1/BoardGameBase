using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmiEnemy : Characters {

	// Use this for initialization
	void Start () {
        isPlayer = false;
	}

    public override bool[,] PossibleMove()
    {

        bool[,] r = new bool[BoardManager.Instance.getBoardSizeX(), BoardManager.Instance.getBoardSizeY()];
        Characters c, c2;


        //Character movement

        //Check Diagonal Left Up
        if (CurrentX >= 1 && CurrentY < BoardManager.Instance.getBoardSizeY() - 1)
        {
            c = BoardManager.Instance.Characters[CurrentX - 1, CurrentY + 1];
            if (c == null)
            {
                r[CurrentX - 1, CurrentY + 1] = true;
            }
        }

        //Check Diagonal Left Down
        if (CurrentX >= 1 && CurrentY >= 1)
        {
            c = BoardManager.Instance.Characters[CurrentX - 1, CurrentY - 1];
            if (c == null)
            {
                r[CurrentX - 1, CurrentY - 1] = true;
            }
        }

        //Check Diagonal Right Down
        if (CurrentX < BoardManager.Instance.getBoardSizeX() - 1 && CurrentY >= 1)
        {
            c = BoardManager.Instance.Characters[CurrentX + 1, CurrentY - 1];
            if (c == null)
            {
                r[CurrentX + 1, CurrentY - 1] = true;
            }
        }

        //Check Diagonal Right Up
        if (CurrentX < BoardManager.Instance.getBoardSizeX() - 1 && CurrentY < BoardManager.Instance.getBoardSizeY() - 1)
        {
            c = BoardManager.Instance.Characters[CurrentX + 1, CurrentY + 1];
            if (c == null)
            {
                r[CurrentX + 1, CurrentY + 1] = true;
            }
        }

        //Move up
        if (CurrentY < BoardManager.Instance.getBoardSizeY() - 1)
        {
            c = BoardManager.Instance.Characters[CurrentX, CurrentY + 1];
            if (c == null)
            {
                r[CurrentX, CurrentY + 1] = true;
            }
        }

        //Move Down
        if (CurrentY > 0)
        {
            c = BoardManager.Instance.Characters[CurrentX, CurrentY - 1];
            if (c == null)
            {
                r[CurrentX, CurrentY - 1] = true;
            }
        }

        //Move Left
        if (CurrentX > 0)
        {
            c = BoardManager.Instance.Characters[CurrentX - 1, CurrentY];
            if (c == null)
            {
                r[CurrentX - 1, CurrentY] = true;
            }
        }

        //Move Right
        if (CurrentX < BoardManager.Instance.getBoardSizeX() - 1)
        {
            c = BoardManager.Instance.Characters[CurrentX + 1, CurrentY];
            if (c == null)
            {
                r[CurrentX + 1, CurrentY] = true;
            }
        }
        return r;
    }

    public void EnemyMove()
    {


    }
}
