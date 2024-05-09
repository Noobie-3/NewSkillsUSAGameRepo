using GameDevSkills;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnTimeON : MonoBehaviour
{
    public bool IsinsideTrigger;
    public Animator Ani;
    public GameObject Partical;
    public bool IsQuest;
    public int QuestID;
    // Start is called before the first frame update
    private void Start()
    {
  
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameController.instance.Player.gameObject)
        {

            GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().canRewind = true;
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
