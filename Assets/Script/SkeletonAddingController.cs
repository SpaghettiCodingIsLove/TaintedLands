using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAddingController : MonoBehaviour
{

    public GameObject skeletonMonster;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= 50; i++)
        {
            CreateSkeletonMonster();
        }
    }

    void CreateSkeletonMonster()
    {
        Vector3 vector = new Vector3(Random.Range(1769.0f, 3100.0f), 170.0f, Random.Range(1411.0f, 3000.0f));
        Instantiate(skeletonMonster, vector, skeletonMonster.transform.rotation);
    }
}
