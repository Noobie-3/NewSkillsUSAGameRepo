using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public GameObject Target;
    public float MoveSpeed;
    // Update is called once per frame
    void Update()
    {
        Camera cam = Camera.main;
        transform.LookAt(Target.transform);
        transform.RotateAround(Target.transform.position, Vector3.up, MoveSpeed * Time.deltaTime);
    }
}
