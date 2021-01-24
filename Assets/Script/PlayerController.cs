using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    private string MoveInputAxis = "Vertical";
    private string TurnInputAxis = "Horizontal";

    public GameObject SpellSpawner;
    public GameObject MagicSpell1;
    public GameObject MagicSpell2;
    public GameObject MagicSpell3;
    

    // rotation that occurs in angles per second holding down input
    public float rotationRate = 150;

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
        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetBool("attakl", true);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            anim.SetBool("attakl", false);
            GameObject TemporaryBulletHandler;
            TemporaryBulletHandler = Instantiate(MagicSpell1, SpellSpawner.transform.position, SpellSpawner.transform.rotation) as GameObject;
            //TemporaryBulletHandler.transform.Rotate(Vector3.left * 90);
            Rigidbody temporaryRigidbody;
            temporaryRigidbody = TemporaryBulletHandler.GetComponent<Rigidbody>();
            temporaryRigidbody.AddForce(transform.forward * 30);
            Destroy(TemporaryBulletHandler, 5.0f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            anim.SetBool("attakr", true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            anim.SetBool("attakr", false);
            GameObject TemporaryBulletHandler;
            TemporaryBulletHandler = Instantiate(MagicSpell2, SpellSpawner.transform.position, SpellSpawner.transform.rotation) as GameObject;
            //TemporaryBulletHandler.transform.Rotate(Vector3.left * 90);
            Rigidbody temporaryRigidbody;
            temporaryRigidbody = TemporaryBulletHandler.GetComponent<Rigidbody>();
            temporaryRigidbody.AddForce(transform.forward * 30);
            Destroy(TemporaryBulletHandler, 5.0f);
        }
        if (Input.GetKey(KeyCode.T))
        {
            anim.SetBool("attakr", true);
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            anim.SetBool("attakr", false);
            GameObject TemporaryBulletHandler;
            TemporaryBulletHandler = Instantiate(MagicSpell3, SpellSpawner.transform.position, SpellSpawner.transform.rotation) as GameObject;
            //TemporaryBulletHandler.transform.Rotate(Vector3.left * 90);
            Rigidbody temporaryRigidbody;
            temporaryRigidbody = TemporaryBulletHandler.GetComponent<Rigidbody>();
            temporaryRigidbody.AddForce(transform.forward * 30);
            Destroy(TemporaryBulletHandler, 5.0f);
        }
        if (Input.GetKey(KeyCode.R))
        {
            anim.SetBool("spell1",true);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            anim.SetBool("spell1",false);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (anim.GetBool("weaponOut") == false)
            {
                anim.SetBool("weaponOut", true);
            }
            else
            {
                anim.SetBool("weaponOut", false);
            }
        }

    }

    private void Turn(float input)
    {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }
}
