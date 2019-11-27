using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonHandTarget : MonoBehaviour
{
    public Transform head;
    public Transform headset;
    public Transform controller;
    private Vector3 offset;
    private Transform defaultTransform;

    void Start()
    {
        defaultTransform = transform;
    }
    void Update()
    {
        offset = headset.position - controller.position;
        transform.position = head.position - offset;
        //transform.rotation = controller.rotation;
        //transform.eulerAngles += defaultTransform.eulerAngles;
    }
}
