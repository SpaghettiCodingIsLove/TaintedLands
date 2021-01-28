using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : MonoBehaviour
{
    private float BulletForwardForce = 300f;
    public GameObject daggerBody;
    public GameObject daggerLeftHand;
    public GameObject daggerRightHand;

    public GameObject particleGeneric1;
    public GameObject positionGeneric1;

    public GameObject[] particlesGeneric2;
    public GameObject[] lightsGeneric2;

    public GameObject[] particlesGeneric3;
    public GameObject[] lightsGeneric3;

    public GameObject particleBarbarian1;
    public GameObject positionBarbarian1;

    public void TakeDaggerOut(string hand)
    {
        daggerBody.SetActive(false);
        if (hand == "Left")
        {
            daggerLeftHand.SetActive(true);
            daggerRightHand.SetActive(false);
        }
        else if (hand == "Right")
        {
            daggerLeftHand.SetActive(false);
            daggerRightHand.SetActive(true);
        }
    }

    public void PutBackDagger(string hand)
    {
        daggerBody.SetActive(true);
        daggerRightHand.SetActive(false);
        daggerLeftHand.SetActive(false);
    }

    public void StartCastGeneric1()
    {
        InvokeRepeating("CastGeneric1", 0.0f, 1.5f);
    }

    public void StopCastGeneric1()
    {
        CancelInvoke("CastGeneric1");
    }

    public void CastGeneric1()
    {
        GameObject TemporaryBulletHandler;
        TemporaryBulletHandler = Instantiate(particleGeneric1, positionGeneric1.transform.position, positionGeneric1.transform.rotation) as GameObject;
        //TemporaryBulletHandler.transform.Rotate(Vector3.left * 90);
        Rigidbody temporaryRigidbody;
        temporaryRigidbody = TemporaryBulletHandler.GetComponent<Rigidbody>();
        temporaryRigidbody.AddForce(transform.forward * BulletForwardForce);
        Destroy(TemporaryBulletHandler, 2.0f);

        /*GameObject newSpell = Instantiate(particleGeneric1, positionGeneric1.transform.position, Quaternion.identity);
        newSpell.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        Destroy(newSpell, 5.0f);*/
    }
}
