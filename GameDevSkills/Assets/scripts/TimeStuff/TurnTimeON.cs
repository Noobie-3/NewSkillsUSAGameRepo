using GameDevSkills;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnTimeON : MonoBehaviour
{
    public NewThirdPerson ntp;
    public bool IsinsideTrigger;
    public Animator Ani;
    public GameObject Partical;
    public bool IsQuest;
    public int QuestID;
    // Start is called before the first frame update
    private void Start()
    {
  
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player_01"))
        {

            ntp.canRewind = true;
            Ani.SetBool("Button_Pressed", false);
            Instantiate(Partical,transform.position, Quaternion.identity);
            Destroy(gameObject.transform.parent.gameObject);

            if(IsQuest)
            {
                QuestManager.Instance.CompleteQuest(QuestID);
            }



        }
            
    
    }

    
}
