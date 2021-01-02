using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWizardController : MonoBehaviour
{
    public float lookRadius = 35f;

    public GameObject Bullet_Emitter1;
    public GameObject Bullet;
    public GameObject Bullet2;
    public float BulletForwardForce;

    Transform target;
    NavMeshAgent agent;
    private Animator animator;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);
        animator = GetComponent<Animator>();
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            if(Time.time - time > 3f)
            {
                FaceTarget();
                animator.Play("Base Layer.idle_combat");
                if (Random.Range(1, 2) == 1)
                {
                    GameObject TemporaryBulletHandler;
                    TemporaryBulletHandler = Instantiate(Bullet, Bullet_Emitter1.transform.position, Bullet_Emitter1.transform.rotation) as GameObject;
                    TemporaryBulletHandler.transform.Rotate(Vector3.left * 90);
                    Rigidbody temporaryRigidbody;
                    temporaryRigidbody = TemporaryBulletHandler.GetComponent<Rigidbody>();
                    temporaryRigidbody.AddForce(transform.forward * BulletForwardForce);
                    Destroy(TemporaryBulletHandler, distance);
                    
                }
                else
                {
                    GameObject TemporaryBulletHandler;
                    TemporaryBulletHandler = Instantiate(Bullet2, Bullet_Emitter1.transform.position, Bullet_Emitter1.transform.rotation) as GameObject;
                    TemporaryBulletHandler.transform.Rotate(Vector3.left * 90);
                    Rigidbody temporaryRigidbody;
                    temporaryRigidbody = TemporaryBulletHandler.GetComponent<Rigidbody>();
                    temporaryRigidbody.AddForce(transform.forward * BulletForwardForce);
                    Destroy(TemporaryBulletHandler, distance);
                }
                time = Time.time;
            }

            if (distance <= agent.stoppingDistance)
            {
                if(Time.time - time > 1.5f)
                {
                    animator.Play("Base Layer.idle_combat");
                    time = Time.time;
                }
            }

            agent.SetDestination(target.position);
            animator.Play("Base Layer.move_forward");
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
