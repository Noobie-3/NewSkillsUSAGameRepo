using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public float Length;
    public bool IsPressed;
    // Start is called before the first frame update
    public void BUttonPressLength( )
    {
        if (Length > 0)
        {
            Length -= Time.deltaTime;
        }
        else if (Length <= 0) {
            IsPressed = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsPressed = true;
        }
        else
        {
            IsPressed = false;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsPressed = false;
        }
    }
}
