using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_with_object : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameController.instance.Player.gameObject)
        {
            other.gameObject.transform.SetParent(transform);
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameController.instance.Player.gameObject)
        {
            other.gameObject.transform.SetParent(null);
            other.GetComponent<Rigidbody>().isKinematic = false;

        }
    }
}
