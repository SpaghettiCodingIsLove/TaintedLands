using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimController : MonoBehaviour
{
    public float moveSpeed = 0.3f;
    public float rotSpeed = 100f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    private GameObject player;

    private float health = 50;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        /*bool walking = Input.GetKey(KeyCode.W);

        animator.SetBool("Walking", walking);

        if(Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("Attack");
        }*/

        if(isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if(isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if (isWalking == true && Vector3.Distance(this.transform.position, player.transform.position) <= 10)
        {
            animator.SetBool("Walking", isWalking);
            this.transform.LookAt(player.transform);
            this.transform.position += transform.forward * moveSpeed * Time.deltaTime;
            animator.SetTrigger("Attack");
        }
        if (isWalking == true && Vector3.Distance(transform.position, player.transform.position) > 10)
        {
            transform.position += transform.forward * moveSpeed;
            animator.SetBool("Walking", isWalking);
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

    private void TakeDamage(float d)
    {
        health -= d;
        if (health <= 0)
            Destroy(this.gameObject);
    }
}
