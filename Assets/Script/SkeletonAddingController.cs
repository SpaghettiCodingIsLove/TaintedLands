using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAddingController : MonoBehaviour
{

    public GameObject skeletonMonster;
    public GameObject male;
    public GameObject female;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            CreateSkeletonMonster();
        }

        for (int i = 0; i < 50; i++)
        {
            CreateSkeletonMonster2();
        }

        for (int i = 0; i < 25; i++)
        {
            CreateMaleCitizen();
        }

        for(int i = 0; i < 25; i++)
        {
            CreateFemaleCitizen();
        }
    }

    void CreateSkeletonMonster()
    {
        Vector3 vector = new Vector3(Random.Range(1769.0f, 3100.0f), 170.0f, Random.Range(1411.0f, 3000.0f));
        Instantiate(skeletonMonster, vector, skeletonMonster.transform.rotation);
    }

    void CreateSkeletonMonster2()
    {
        Vector3 vector = new Vector3(Random.Range(880.0f, 2201.0f), 170.0f, Random.Range(1173.0f, 3116.0f));
        Instantiate(skeletonMonster, vector, skeletonMonster.transform.rotation);
    }

    void CreateMaleCitizen()
    {
        Vector3 vector = new Vector3(Random.Range(2553.0f, 2785.0f), 100.0f, Random.Range(501.0f, 900.0f));
        Instantiate(male, vector, male.transform.rotation);
    }

    void CreateFemaleCitizen()
    {
        Vector3 vector = new Vector3(Random.Range(2553.0f, 2785.0f), 100.0f, Random.Range(501.0f, 900.0f));
        Instantiate(female, vector, female.transform.rotation);
    }
}
