using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimController : MonoBehaviour
{
    public float lookRadius = 10f;

    public float moveSpeed = 0.3f;
    public float rotSpeed = 100f;
    private float time;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    Transform target;

    private float health = 50;
    
    UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.Warp(transform.position);
        target = PlayerManager.instance.player.transform;
        animator = GetComponent<Animator>();
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, target.position);

        /*if(distance <= 10 && distance > 2)
        {
            animator.Play("Animation.DS_onehand_walk");
            this.transform.LookAt(target);
            this.transform.position += transform.forward * moveSpeed;
        }
        else if(distance <= 2)
        {
            animator.Play("Animation.DS_onehand_attack_A");
        }*/
        if (distance <= lookRadius && distance > 2f)
        {
            FaceTarget();
            animator.SetBool("Walking", true);
        }
        else if (distance <= 2f)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Attack", true);
            time = Time.time;
            Debug.Log("Szkieletor powinien zaatakować");
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
                animator.SetBool("Walking", isWalking);
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
        if(rotateLorR == 1)
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

    private void TakeDamage(float d)
    {
        health -= d;
        if (health <= 0)
        {
            PlayerManager.instance.player.GetComponent<PlayerStats>().AddMoney(10);
            PlayerManager.instance.player.GetComponent<PlayerStats>().NumOfKills++;
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("spell"))
        {
            TakeDamage(25);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
