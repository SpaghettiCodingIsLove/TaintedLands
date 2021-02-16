using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TigerWalking : MonoBehaviour
{
    public float lookRadius = 10f;

    public float moveSpeed = 0.1f;
    public float rotSpeed = 50f;
    private float time;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    private bool isAttacking = false;
    AudioSource audio;
    [SerializeField]
    public AudioClip hit;
    [SerializeField]
    public AudioClip snarl;
    private int HP = 2;

    NavMeshAgent agent;
    Transform target;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);
        target = PlayerManager.instance.player.transform;
        animator = GetComponent<Animator>();
        time = Time.time;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, target.position);

        if (distance <= lookRadius && distance > 2.0f)
        {
            FaceTarget();
            agent.SetDestination(target.position);
            animator.Play("Base Layer.run");
            
        }
        else if(distance <= 2.0f)
        {
            
            animator.enabled = false;
            agent.ResetPath();
            animator.Play("Base Layer.hit");
            audio.PlayOneShot(snarl);
            /*if (Time.time - time > 1.5f)
            {
                animator.Play("Base Layer.hit");
                time = Time.time;
            }*/
        }
        else
        {
            if (isWandering == false)
            {
                StartCoroutine(Wander());
            }
            if (isRotatingRight == true)
            {
                transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
            }
            if (isRotatingLeft == true)
            {
                transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
            }
            if (isWalking == true)
            {
                transform.position += transform.forward * moveSpeed;
                animator.Play("Base Layer.walk");
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("spell"))
        {
            HP -= 1;
            Destroy(collision.gameObject);
            audio.PlayOneShot(hit);
            if (HP == 0)
            {
                Destroy(gameObject);
                PlayerManager.instance.player.GetComponent<PlayerStats>().currentExp += 1;
            }
        }
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(1, 2);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if (rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isWandering = false;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    
}
