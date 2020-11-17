using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition = new Vector3(-2.85f, 0.75f, 0.0f);

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    private void Update()
    {
        if(target == null)
        {
            Debug.LogWarning("Missing Target");
            return;
        }

        if(offsetPositionSpace == Space.Self)
        {
            transform.position = target.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = target.position + offsetPosition;
        }

        if(lookAt)
        {
            transform.LookAt(target);
        }    
        else
        {
            transform.rotation = target.rotation;
        }
    }
}
