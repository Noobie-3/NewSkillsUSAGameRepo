using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_with_object : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent = transform;
                }
    }
    private void OnTriggerExit(Collider other)
    {
        other.gameObject.transform.SetParent(transform, false);
        print("Test");
    }
}
