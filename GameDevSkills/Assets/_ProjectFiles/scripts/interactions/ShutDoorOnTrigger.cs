using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutDoorOnTrigger : MonoBehaviour
{
    public Animator anim;
    public bool OneTimeUse;


    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("HurtBox")) // Compare the tags to determine if it's a prefab in the list
            {
                anim.SetBool("Button_Pressed", true);
                if(OneTimeUse)
                {
                gameObject.SetActive(false);
                }
                print(other.tag + "Found this tag");
        }
        else
        {
            print(other.tag);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Enemy") // Compare the tags to determine if it's a prefab in the list
        {
            anim.SetBool("Button_Pressed", false);
        }
    }

}
