using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_script : MonoBehaviour
{
    CharacterController charCtrl;
    Animator animCtrl;
    Camera camCtrl;
    private const string ANIM_WALK = "isWalking";
    private const string ANIM_RUN = "isRunning";
    
    public float walkSpeed = 0.1f;
    public float runSpeed = 1;

    public float gravity = Physics.gravity.y;
    public float _rotationSpeed = 180;
    public Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        animCtrl = GetComponent<Animator>();
        camCtrl = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Move();
        

    }
    private void Rotate()
    {
        moveDirection = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
        transform.localEulerAngles = moveDirection;

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
            moveDirection = Camera.main.transform.TransformDirection(dir);
            //transform.TransformDirection(dir);         
        }

        moveDirection.y -= gravity * Time.deltaTime;
        charCtrl.Move(moveDirection*speed * Time.deltaTime);
        //transform.Translate(new Vector3(0, -gravity * Time.deltaTime, horizontalMove + verticalMove != 0 ? 1 : 0) * speed * Time.deltaTime);
    }
}
