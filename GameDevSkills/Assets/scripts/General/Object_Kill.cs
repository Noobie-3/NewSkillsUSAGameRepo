using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class Object_Kill : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Kill_Check_Objects")) {
            Destroy(other.gameObject.transform.parent);
        }
    }
}
