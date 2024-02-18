using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Effect_Popup : MonoBehaviour
{
    public Animation anim;
 public void TurnOff()
    {
        gameObject.SetActive(false);

    }

    public void TurnOn() { 
    
        gameObject.SetActive(true);
    }

}
