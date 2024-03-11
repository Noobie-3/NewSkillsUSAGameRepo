using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{ 

    public float speed;
    public GameController Gc;
    private void Start()
    {
        Gc = GameObject.FindWithTag("GC").GetComponent<GameController>();
    }
    void FixedUpdate()
    {if(Gc.isDead == false)
        {
            GetComponent<Rigidbody>().position -= transform.forward * speed * Time.deltaTime;
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * speed * Time.deltaTime);

        }


    }

}