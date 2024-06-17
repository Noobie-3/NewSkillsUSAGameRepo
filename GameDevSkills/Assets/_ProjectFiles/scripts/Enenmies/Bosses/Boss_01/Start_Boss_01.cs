using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Start_Boss_01 : MonoBehaviour
{
    public MoveAlongwayPoints[] PlatformsToMove;
    public Boss_StateMachine Boss;
    public Animator[] animator;
    public Animator DropDown;
    public bool FirstTrigger;
    public Npc_Basic Npc;
    private bool delayStarted;
    public float DelayTime;
    public float Delay;
    public float Delay1;
    public float Delay2;
    public bool DelayTriggered = false;
    public bool Delay1Triggered = false;
    public bool Delay2Triggered = false;
    public Animator Boss_Entrance_animation;
    public CinemachineVirtualCamera Cam_Boss;

    private void OnTriggerEnter(Collider other)
    {if (other.gameObject == GameController.instance.Player && FirstTrigger == false)
        {
            if(Cam_Boss != null)
            {
                Cam_Boss.Priority = 11;
            }
            GameController.instance.IsPaused = true;
            GameController.instance.BossBar.gameObject.SetActive(true);
            GameController.instance.BossBar.GetComponentInChildren<TextMeshProUGUI>().text = Boss.stats.Name;
            delayStarted = true;
            foreach (MoveAlongwayPoints Platform in PlatformsToMove)
            {
                Platform.CanMove = true;
            }



            foreach (Animator anim in animator)
            {
                anim.SetBool("Button_Pressed", false);
            }
            DropDown.SetBool("CanMove", true);

            FirstTrigger = true;
        }


    }
    private void OnTriggerStay(Collider other)
    {

    }


    public void DelayItems()
    {
        DelayTime -= Time.deltaTime;
        if (DelayTime <= Delay2 && DelayTriggered == false) {
            Cam_Boss.Priority = 9;
            Boss.BossFightStarted = true;
            DelayTriggered = true;
            GameController.instance.IsPaused = false;
            GameController.instance.MusicManager.ChangeMainMusic(Boss.stats.BossMusic);
        }
        else if (DelayTime <= Delay1 && Delay1Triggered == false)
        {
            if (Boss_Entrance_animation != null)
            {
                Boss_Entrance_animation.SetBool("Boss_Start", true);
            }
            if (!Npc.isTalking)
            {
                Npc.gameObject.SetActive(true);
                Npc.CanTalk = true;
            }
            Delay1Triggered = true;

        }
        else if(DelayTime <= Delay &&  Delay2Triggered == false)
        {
            //     GameController.instance.IsPaused = true;

            foreach (MoveAlongwayPoints Platform in PlatformsToMove)
            {
                Platform.CanReturn = true;
            }
            Delay2Triggered = true;
        }

    }

    private void Update()
    {
        if(delayStarted && !DelayTriggered)
        {

                if (Npc.isTalking == false)
                {
                    DelayItems();
                }

            
        }
    }

}
