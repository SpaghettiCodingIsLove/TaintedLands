using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private int HP = 30;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("spell"))
        {
            Destroy(collision.gameObject);
            HP -= 10;
            if(HP == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
