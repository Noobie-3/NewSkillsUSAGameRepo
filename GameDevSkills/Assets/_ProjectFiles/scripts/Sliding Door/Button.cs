using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public float Length;
    public float DefaultLength;
    public bool IsPressed;

    // Start is called before the first frame update
    public void BUttonPressLength( )
    {
        if (Length > 0)
        {
            Length -= Time.deltaTime;
            IsPressed = true;
        }
        else if (Length <= 0) {
            IsPressed = false;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player_01")
        {
            IsPressed = true;
            Length = DefaultLength;
        }
        else
        {
            IsPressed = false;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player_01")
        {
            Length = DefaultLength;
        }
    }
    private void Update()
    {
        if (Length > 0) {
            BUttonPressLength();
        }
        else if(Length <= 0)
        {
            IsPressed = false;
        }
        

        
    }
}
