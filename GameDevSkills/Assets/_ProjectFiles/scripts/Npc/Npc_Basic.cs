using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Npc_Basic : MonoBehaviour
{
    [SerializeField] private string npcName;
    [SerializeField] private string[] dialogues;
    [SerializeField] private bool isOneTimeDialogue = false;
    [SerializeField] private string OneTimeDiologe;
    [SerializeField] private string CurrentDialogue;
    [SerializeField] private float dialogueTime = 3f;
    private float dialogueTimeDelayPerChar;
    [SerializeField] private int CurrentDialogueIndex;
    public string CurrentText; 

    [SerializeField] private GameObject PopUpBox;
    public bool isTalking;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameController.instance.Player)
        {
            NpcTalk_Animate.instance.gameObject.SetActive(true);
            if (isOneTimeDialogue)
            {
                dialogueTimeDelayPerChar = TimePerChar(OneTimeDiologe);
                CurrentDialogue = OneTimeDiologe;
                StartCoroutine(StartDialogue(OneTimeDiologe));
                isOneTimeDialogue = false;
            }
            else if ((!isOneTimeDialogue))
            {
                dialogueTimeDelayPerChar = TimePerChar(dialogues[CurrentDialogueIndex]);
                CurrentDialogue = dialogues[CurrentDialogueIndex];
                StartCoroutine(StartDialogue(CurrentDialogue));
            }

        }
    }

   private void  OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameController.instance.Player)
        {
            StopCoroutine(StartDialogue(CurrentDialogue));
            CurrentText = "";
            isTalking = false;
        }
    }
    IEnumerator StartDialogue(string text)
    {
        NpcTalk_Animate.instance.npc_Basic = this;
        NpcTalk_Animate.instance.gameObject.SetActive(true);

        isTalking = true;
         dialogueTimeDelayPerChar = TimePerChar(text);
        foreach (char c in text)
        {
            if (CurrentText != text)
            {
                CurrentText = CurrentText + c;
                NpcTalk_Animate.instance.text.text = CurrentText;
            }
/*            Debug.Log(CurrentText);
*/
            yield return new WaitForSeconds(.25f);
        }
        isTalking = false;
        NpcTalk_Animate.instance.LoopOn = true;

    }

    private float TimePerChar(string text)
    {
        return text.Length / dialogueTime;
    }
    private void Update()
    {
        if (isTalking)
        {
/*            Debug.Log("Talking" + CurrentText);
*/            //Animate
        }
        else if (!isTalking && CurrentText != "")
        {
/*            Debug.Log("Not Talking");
*/            CurrentText = "";
        }
    }
}
