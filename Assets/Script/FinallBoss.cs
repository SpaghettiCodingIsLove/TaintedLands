using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinallBoss : MonoBehaviour
{
    public float lookRadius;

    public int HP;
    private bool isDead = false;
    private float deadTime;

    public ParticleSystem Damage;
    public ParticleSystem Attack;

    public GameObject Bullet_Emitter1;
    public GameObject Bullet;
    public GameObject Bullet2;
    public float BulletForwardForce;

    Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.Warp(transform.position);
        animator = GetComponent<Animator>();
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (isDead && Time.time - deadTime > 5)
        {
            SaveSystem.isFinallBossDead = true;
            Destroy(gameObject);
        }

        if (distance <= lookRadius && !isDead)
        {
            if (Time.time - time > 3f)
            {
                FaceTarget();
                animator.Play("Base Layer.idle_combat");
                if (Random.Range(1, 2) == 1)
                {
                    Attack.Play();
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
                    Attack.Play();
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
                if (Time.time - time > 1.5f)
                {
                    animator.Play("Base Layer.idle_combat");
                    time = Time.time;
                }
            }

            agent.SetDestination(target.position);
            animator.Play("Base Layer.move_forward");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("spell"))
        {
            Destroy(collision.gameObject);
            HP = HP - 10;
            if(HP > 0)
                Damage.Play();
            animator.Play("Base Layer.damage_001");
            if (HP == 0)
            {
                PlayerManager.instance.player.GetComponent<PlayerStats>().AddMoney(250);
                isDead = true;
                deadTime = Time.time;
                animator.Play("Base Layer.dead");
            }
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
