using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    
    private Animator anim; 
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("iswalking", true);
            //anim.Play("iswalking");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("iswalkingbackward", true);
            //anim.Play("iswalkingbackward");
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(transform.up * Time.deltaTime * -100f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(transform.up * Time.deltaTime * 100f);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("isrunning", true);
            //anim.Play("isrunning");
            
        }
        else
        {
            anim.Play("Idle");
        }
    }
}
