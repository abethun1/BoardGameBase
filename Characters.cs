using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Characters : MonoBehaviour {

    public int CurrentX { set; get; }
    public int CurrentY { set; get; }

    public bool isPlayer;
    public Animator animatorController;
    public Rigidbody rigidBody;
    public CapsuleCollider capsuleCollider;


    static Quaternion playerOrientation = Quaternion.Euler(0, 0, 0);
    static Quaternion enemyOreintation = Quaternion.Euler(0, 180, 0);

    public virtual void Start()
    {
        animatorController = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody>();
        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();

        capsuleCollider.enabled = false;
    }
/*
    private void FixedUpdate()
    {
        if (getIsMoving())
        {
            if (walking)
                animatorController.SetBool("Walk", true);
            else if (running)
                animatorController.SetBool("Run", true);
            Vector3 movement = BoardManager.Instance.getMovingDirection();
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            Quaternion newRotation = Quaternion.Lerp(rigidBody.rotation, targetRotation, 3 * Time.deltaTime);
            rigidBody.MoveRotation(newRotation);
        }
        else if (!turning)
        {
            if (isWhite)
                rigidBody.MoveRotation(whiteOrientation);
            else
                rigidBody.MoveRotation(blackOrientation);

            animatorController.SetBool("Walk", false);
            animatorController.SetBool("Run", false);
        }
    }
  */
    public void SetPosition(int x, int y)
    {
        Debug.Log(animatorController);
        CurrentX = x;
        CurrentY = y;
    }

    public virtual bool[,] PossibleMove()
    {
        return new bool[BoardManager.Instance.getBoardSizeX(), BoardManager.Instance.getBoardSizeY()];
    }
}
