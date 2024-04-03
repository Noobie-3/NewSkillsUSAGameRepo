using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Npc_Basic : MonoBehaviour
{
    [SerializeField] private string npcName;
    [SerializeField] private string[] dialogues;
    [SerializeField] private bool isOneTimeDialogue = false;
    [SerializeField] private string OneTimeDiologe;
    [SerializeField] private string CurrentDialogue;
    [SerializeField] private float dialogueTime = 3f;
    public float dialogueTimeDelayPerChar;
    [SerializeField] private int CurrentDialogueIndex;
    public string CurrentText;
    public Image[] AnimFrames;
    public GameObject Indicator;

    [SerializeField] private GameObject PopUpBox;
    public bool isTalking;

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject == GameController.instance.Player)
        {
            if(Indicator != null )
            {
                //keybind INdicator for interacting
                Indicator.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E) && isTalking == false)
            {
                //Turn On Game Object And set npcAnimation to what ever the current animation set is 
                NpcTalk_Animate.instance.npcAnimations = AnimFrames;
                NpcTalk_Animate.instance.gameObject.SetActive(true);

                //first time meeting NPC
                if (isOneTimeDialogue)
                {
                    dialogueTimeDelayPerChar = TimePerChar(OneTimeDiologe);
                    CurrentDialogue = OneTimeDiologe;
                    StartCoroutine(StartDialogue(OneTimeDiologe));

                }
                //Every other Time Meeting Npc
                else if ((!isOneTimeDialogue))
                {

                    if (CurrentDialogueIndex == dialogues.Length)
                    {
                        CurrentDialogueIndex = 0;
                        ResetText();
                        TurnOffText();
                    }

                    else if (CurrentDialogueIndex <= dialogues.Length - 1)
                    {
                        dialogueTimeDelayPerChar = TimePerChar(dialogues[CurrentDialogueIndex]);
                        CurrentDialogue = dialogues[CurrentDialogueIndex];
                        StartCoroutine(StartDialogue(CurrentDialogue));
                    }

                }


            }

        }
        
    }

   private void  OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameController.instance.Player)
        {//turn off everything and reset the npc text
            StopCoroutine(StartDialogue(CurrentDialogue));
            ResetText();
            isTalking = false;
            Indicator.SetActive(false);
        }
    }
    IEnumerator StartDialogue(string text)
    {
        ResetText();//clear old text

        //Start Animating
        NpcTalk_Animate.instance.npc_Basic = this;
        NpcTalk_Animate.instance.gameObject.SetActive(true);

        isTalking = true;

        dialogueTimeDelayPerChar = TimePerChar(text);//determined the speed of which each char will appear
        foreach (char c in text)
        {
            //makes the chars appear at above speed
            if (CurrentText != text)
            {
                CurrentText = CurrentText + c;
                NpcTalk_Animate.instance.text.text = CurrentText;
            }
            yield return new WaitForSeconds(dialogueTimeDelayPerChar);
        }
        isTalking = false;// No longer talking

        NpcTalk_Animate.instance.LoopOn = true;//Keep Animating

        //increase index if not onetime diologe
        if(CurrentDialogueIndex < dialogues.Length && isOneTimeDialogue == false)
        {
            CurrentDialogueIndex++;
        }

        else if(CurrentDialogueIndex == dialogues.Length -1)
        {
            CurrentDialogueIndex = 0;
        }

        isOneTimeDialogue = false;

    }

    private float TimePerChar(string text)
    {
        return dialogueTime / text.Length;//time per char to appear when the npc is talking
    }

    private void ResetText()
    {
        CurrentText = "";
    }

    private void TurnOffText()
    {
        NpcTalk_Animate.instance.gameObject.SetActive(false);
    }
    
}
