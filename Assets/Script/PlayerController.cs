using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    private string MoveInputAxis = "Vertical";
    private string TurnInputAxis = "Horizontal";
    

    // rotation that occurs in angles per second holding down input
    public float rotationRate = 360;

    // units moved per second holding down move input
    private CharacterController controller;

    private float verticalVelocity;
    private float gravity = 14.0f;
    private float jumpForce = 10.0f;
    

    private Rigidbody rb;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
 
        float turnAxis = Input.GetAxis(TurnInputAxis);

        Turn(turnAxis);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("iswalking", true);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("isrunning", true);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.SetBool("isrunning", false);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("isrunning", false);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            anim.SetBool("iswalking", false);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("iswalkingbackward",true);
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            anim.SetBool("iswalkingbackward", false);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetInteger("jump", 1);
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetInteger("jump", 0);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("attackl");

        }   
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("attackr");

        }
        if (Input.GetKey(KeyCode.R))
        {
            anim.SetBool("spell1",true);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            anim.SetBool("spell1",false);
        }



    }

    private void Turn(float input)
    {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }
}
