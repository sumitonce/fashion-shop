using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 moveOffset;
    public float followSpeed = 5f;

    private void LateUpdate()
    {
        transform.position = target.position + moveOffset;
    }
}
