using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_script : MonoBehaviour
{
    CharacterController charCtrl;
    Animator animCtrl;
    private const string ANIM_WALK = "isWalking";
    private const string ANIM_RUN = "isRunning";
    
    public float walkSpeed = 0.1f;
    public float runSpeed = 1;

    public float gravity = Physics.gravity.y;
    public float _rotationSpeed = 180;

    // Start is called before the first frame update
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        animCtrl = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Move();
        

    }

    private void Rotate()
    {
        var turnDirHorizontal = Math.Sign(Input.GetAxis("Horizontal"))*90;
        var turnDirVertical = Input.GetAxis("Vertical") >= 0 ? 0 : 180;
        var currentRotationY = transform.eulerAngles.y;
        if (Input.GetAxis("Horizontal") != 0 && turnDirHorizontal != currentRotationY)
            transform.localRotation = Quaternion.Euler(0, turnDirHorizontal, 0); 
        if(Input.GetAxis("Vertical")!=0 && turnDirVertical != currentRotationY)
            transform.localRotation = Quaternion.Euler(0, turnDirVertical, 0);

    }

    void Move()
    {
        animCtrl.SetBool(ANIM_WALK, false);
        animCtrl.SetBool(ANIM_RUN, false);

        var verticalMove = Input.GetAxis("Vertical");
        var horizontalMove = Input.GetAxis("Horizontal");
        var moveDirection = Vector3.zero;
        var speed = walkSpeed;
        var dir = new Vector3(horizontalMove, 0, verticalMove);
        var isMoved = dir!= Vector3.zero;


        animCtrl.SetBool(ANIM_WALK, isMoved);
       
        if (charCtrl.isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
                animCtrl.SetBool(ANIM_WALK, false);
                animCtrl.SetBool(ANIM_RUN, isMoved);
            }
            moveDirection = dir;
              //transform.TransformDirection(dir);         
        }

        moveDirection.y -= gravity * Time.deltaTime;
        charCtrl.Move(moveDirection*speed * Time.deltaTime);
        //transform.Translate(new Vector3(0, -gravity * Time.deltaTime, horizontalMove + verticalMove != 0 ? 1 : 0) * speed * Time.deltaTime);
    }
}
