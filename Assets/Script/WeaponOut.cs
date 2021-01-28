using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOut : MonoBehaviour
{
    GameObject sword;

    
    

    // Start is called before the first frame update
    void Start()
    {
        sword = GameObject.Find("Sword");
        sword.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
  

    }   

    void weaponOut()
    {
        sword.SetActive(true);
    }

    void weaponBack()
    {
        sword.SetActive(false);
    }
}
