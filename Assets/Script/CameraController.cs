using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float distanceAway = 12f;
    [SerializeField]
    private float mindistanceAway = 10f;
    [SerializeField]
    private float maxdistanceAway = 20f;
    [SerializeField]
    private float zoomspeed = 4f;
    [SerializeField]
    private float distanceUp = 3f;
    [SerializeField]
    private float smooth = 3f;
    [SerializeField]
    private Transform follow;
    private Vector3 targetPosition;

    void Start()
    {
        follow = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        distanceAway += Input.GetAxis("Mouse ScrollWheel") * zoomspeed;
        distanceAway = Mathf.Clamp(distanceAway, mindistanceAway, maxdistanceAway);
    }

    void LateUpdate()
    {


        targetPosition = follow.position + follow.up * distanceUp - follow.forward * distanceAway;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);

        transform.LookAt(follow);
    }

}