using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    
    public Animator anim; 
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.Play("Walk");

        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.Play("Run");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.Play("WalkBack");
        }
        else
        {
            anim.Play("Idle");
        }



    }
}
