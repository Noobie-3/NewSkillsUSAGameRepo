using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{ 

    public float speed;
    public GameController Gc;
    public GameObject Target;
    public Vector3 targetPos;
    private void Start()
    {
        Gc = GameObject.FindWithTag("GC").GetComponent<GameController>();

    }
    void FixedUpdate()
    {if(GameController.instance.IsPaused == false)
        {
            GetComponent<Rigidbody>().position -= transform.forward * speed * Time.deltaTime;
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * speed * Time.deltaTime);

        }


    }
    private void OnTriggerStay(Collider other)
    {

        if(other.gameObject.GetComponent<Rigidbody>())
        {
/*            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            rb.MovePosition(rb.position + targetPos.normalized * speed * Time.fixedDeltaTime);
            rb.velocity = rb.velocity + targetPos.normalized * speed;
*/

        }
    }
    private void OnTriggerExit(Collider other)
    {


    }

}