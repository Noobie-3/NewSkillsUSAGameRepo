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

    [SerializeField] private GameObject PopUpBox;
    public bool isTalking;

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E) && isTalking == false && CurrentText == "")
        {
            if (other.gameObject == GameController.instance.Player)
            {

                NpcTalk_Animate.instance.npcAnimations = AnimFrames;
                NpcTalk_Animate.instance.gameObject.SetActive(true);
                if (isOneTimeDialogue)
                {
                    dialogueTimeDelayPerChar = TimePerChar(OneTimeDiologe);
                    CurrentDialogue = OneTimeDiologe;
                    StartCoroutine(StartDialogue(OneTimeDiologe));
                }
                else if ((!isOneTimeDialogue))
                {
                    dialogueTimeDelayPerChar = TimePerChar(dialogues[CurrentDialogueIndex]);
                    CurrentDialogue = dialogues[CurrentDialogueIndex];
                    StartCoroutine(StartDialogue(CurrentDialogue));
                }

            }

        }
    }

   private void  OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameController.instance.Player)
        {
            StopCoroutine(StartDialogue(CurrentDialogue));
            ResetText();
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
            yield return new WaitForSeconds(dialogueTimeDelayPerChar);
        }
        isTalking = false;
        NpcTalk_Animate.instance.LoopOn = true;

    }

    private float TimePerChar(string text)
    {
        return dialogueTime / text.Length;
    }
    private void Update()
    {
        if (isTalking)
        {
/*            Debug.Log("Talking" + CurrentText);
*/            //Animate
        }
        else if (!isTalking )
        {if(CurrentText != "")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if (isOneTimeDialogue == false)
                    {

                        if (CurrentDialogueIndex < dialogues.Length - 1)
                        {
                            CurrentDialogueIndex++;

                            dialogueTimeDelayPerChar = TimePerChar(dialogues[CurrentDialogueIndex]);
                            CurrentDialogue = dialogues[CurrentDialogueIndex];
                            StartCoroutine(StartDialogue(CurrentDialogue));
                        }
                        else
                        {
                            ResetText();
                        }
                    }

                    else if (isOneTimeDialogue == true)
                    {
                        isOneTimeDialogue = false;
                        ResetText();
                    }
                    else
                    {
                        ResetText();
                    }
                }

            }
            /*            Debug.Log("Not Talking");
            */
        }
    }


    private void Start()
    {
/*        if(AnimParent != null)
        {
            AnimFrames = new GameObject[AnimParent.transform.childCount];
            for (int i = 0; i < AnimParent.transform.childCount; i++)
            {
                AnimFrames[i] = AnimParent.transform.GetChild(i).gameObject;
            }
        }*/
    }

    private void ResetText()
    {
        CurrentText = "";
        NpcTalk_Animate.instance.gameObject.SetActive(false);
    }
    
}
