using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{ 

    public float speed = 2.0f;

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().position -= transform.forward * speed * Time.deltaTime;
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * speed * Time.deltaTime);

    }

}