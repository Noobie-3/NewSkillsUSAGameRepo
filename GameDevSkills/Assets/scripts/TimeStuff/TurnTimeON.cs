using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnTimeON : MonoBehaviour
{
    public NewThirdPerson ntp;
    public bool IsinsideTrigger;
    // Start is called before the first frame update
    private void Start()
    {
  
    }
    private void OnTriggerStay(Collider other)
    {
      if(other.CompareTag("Player"))
        {
            
            ntp.canRewind = true;
            Destroy(gameObject.transform.parent.gameObject);
        }  
    }

    
}
