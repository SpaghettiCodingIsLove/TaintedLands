using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooting : MonoBehaviour
{
    public GameObject Bullet_Emitter1;
    public GameObject Bullet_Emitter2;
    public GameObject Bullet_Emitter3;
    public GameObject Bullet_Emitter4;
    public GameObject Bullet_Emitter5;

    public GameObject Bullet;

    public float BulletForwardForce;

    float lastFired;

    void Start()
    {
        lastFired = Time.time;
    }

    void Update()
    {
        if(Time.time - lastFired > 5)
        {
            GameObject TemporaryBulletHandler;
            TemporaryBulletHandler = Instantiate(Bullet, Bullet_Emitter1.transform.position, Bullet_Emitter1.transform.rotation) as GameObject;
            TemporaryBulletHandler.transform.Rotate(Vector3.left * 90);
            Rigidbody temporaryRigidbody;
            temporaryRigidbody = TemporaryBulletHandler.GetComponent<Rigidbody>();
            temporaryRigidbody.AddForce(transform.forward * BulletForwardForce);
            Destroy(TemporaryBulletHandler, 3.0f);

            GameObject TemporaryBulletHandler2;
            TemporaryBulletHandler2 = Instantiate(Bullet, Bullet_Emitter2.transform.position, Bullet_Emitter2.transform.rotation) as GameObject;
            TemporaryBulletHandler2.transform.Rotate(Vector3.left * 90);
            Rigidbody temporaryRigidbody2;
            temporaryRigidbody2 = TemporaryBulletHandler2.GetComponent<Rigidbody>();
            temporaryRigidbody2.AddForce(transform.forward * BulletForwardForce);
            Destroy(TemporaryBulletHandler2, 2.0f);

            GameObject TemporaryBulletHandler3;
            TemporaryBulletHandler3 = Instantiate(Bullet, Bullet_Emitter3.transform.position, Bullet_Emitter3.transform.rotation) as GameObject;
            TemporaryBulletHandler3.transform.Rotate(Vector3.left * 90);
            Rigidbody temporaryRigidbody3;
            temporaryRigidbody3 = TemporaryBulletHandler3.GetComponent<Rigidbody>();
            temporaryRigidbody3.AddForce(transform.forward * BulletForwardForce);
            Destroy(TemporaryBulletHandler3, 2.0f);

            GameObject TemporaryBulletHandler4;
            TemporaryBulletHandler4 = Instantiate(Bullet, Bullet_Emitter4.transform.position, Bullet_Emitter4.transform.rotation) as GameObject;
            TemporaryBulletHandler4.transform.Rotate(Vector3.left * 90);
            Rigidbody temporaryRigidbody4;
            temporaryRigidbody4 = TemporaryBulletHandler4.GetComponent<Rigidbody>();
            temporaryRigidbody4.AddForce(transform.forward * BulletForwardForce);
            Destroy(TemporaryBulletHandler4, 2.0f);


            GameObject TemporaryBulletHandler5;
            TemporaryBulletHandler5 = Instantiate(Bullet, Bullet_Emitter5.transform.position, Bullet_Emitter5.transform.rotation) as GameObject;
            TemporaryBulletHandler5.transform.Rotate(Vector3.left * 90);
            Rigidbody temporaryRigidbody5;
            temporaryRigidbody5 = TemporaryBulletHandler5.GetComponent<Rigidbody>();
            temporaryRigidbody5.AddForce(transform.forward * BulletForwardForce);
            Destroy(TemporaryBulletHandler5, 2.0f);
            lastFired = Time.time;
        }
        
    }
}
