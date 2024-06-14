using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Npc_Basic : MonoBehaviour
{
    [SerializeField] private string npcName;
    [SerializeField] private string[] dialogues;
    [SerializeField] private bool isOneTimeDialogue = false;
    [SerializeField] private string OneTimeDialogue;
    [SerializeField] private string CurrentDialogue;
    [SerializeField] private float dialogueTime = 3f;
    public float dialogueTimeDelayPerChar;
    [SerializeField] private int CurrentDialogueIndex;
    public string CurrentText;
    public Image[] AnimFrames;
    public GameObject Indicator;
    public bool DestroyAfterTalking;

    private Coroutine CurrentlytalkingCoRoutine;

    [SerializeField] private GameObject PopUpBox;
    public bool isTalking;
    public bool CanTalk;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CanTalk = true;
        }
        if (other.gameObject == GameController.instance.Player)
        {
            if (Indicator != null)
            {
                // Keybind Indicator for interacting
                Indicator.SetActive(true);
            }
            if (isTalking == false && CanTalk)
            {
                if (!GameController.instance.IsPaused)
                {
                    GameController.instance.IsPaused = true;
                }
                // Turn On Game Object and set npcAnimation to whatever the current animation set is 
                NpcTalk_Animate.instance.npcAnimations = AnimFrames;
                NpcTalk_Animate.instance.gameObject.SetActive(true);

                // First time meeting NPC
                if (isOneTimeDialogue)
                {
                    dialogueTimeDelayPerChar = TimePerChar(OneTimeDialogue);
                    CurrentDialogue = OneTimeDialogue;
                    CurrentlytalkingCoRoutine = StartCoroutine(StartDialogue(OneTimeDialogue));
                }
                // Every other time meeting NPC
                else
                {
                    if (CurrentDialogueIndex >= dialogues.Length)
                    {
                        ResetText();
                        TurnOffText();
                        if (DestroyAfterTalking)
                        {
                            Destroy(gameObject);
                        }
                        CurrentDialogueIndex = 0;
                        CanTalk = false;

                    }
                    else
                    {
                        dialogueTimeDelayPerChar = TimePerChar(dialogues[CurrentDialogueIndex]);
                        CurrentDialogue = dialogues[CurrentDialogueIndex];
                        CurrentlytalkingCoRoutine = StartCoroutine(StartDialogue(CurrentDialogue));
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameController.instance.Player)
        {
            // Turn off everything and reset the NPC text
            if (CurrentlytalkingCoRoutine != null)
            {
                StopCoroutine(CurrentlytalkingCoRoutine);
            }
            if (Indicator != null)
            {


                Indicator.SetActive(false);
            }
            ResetText();
            TurnOffText();
            CurrentDialogueIndex = 0;
            isTalking = false;
            GameController.instance.IsPaused = false;
        }
    }

    IEnumerator StartDialogue(string text)
    {
        CanTalk = false;
        ResetText(); // Clear old text

        // Start Animating
        NpcTalk_Animate.instance.npc_Basic = this;
        NpcTalk_Animate.instance.gameObject.SetActive(true);

        isTalking = true;
        dialogueTimeDelayPerChar = TimePerChar(text); // Determine the speed at which each char will appear
        foreach (char c in text)
        {
            // Make the chars appear at above speed
            if (CurrentText != text && isTalking == true)
            {
                CurrentText += c;
                NpcTalk_Animate.instance.text.text = CurrentText;
            }
            yield return new WaitForSeconds(dialogueTimeDelayPerChar);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E)); // Wait for player input before continuing

        isTalking = false; // No longer talking
        GameController.instance.IsPaused = false;

        NpcTalk_Animate.instance.LoopOn = true; // Keep Animating

        if (isOneTimeDialogue)
        {
            isOneTimeDialogue = false; // Disable one-time dialogue for future interactions
        }
        else
        {
            // Increase index if not one-time dialogue
            if (CurrentDialogueIndex < dialogues.Length)
            {
                CurrentDialogueIndex++;
            }

            // Check if dialogue loop is complete
            if (CurrentDialogueIndex >= dialogues.Length)
            {
                ResetText();
                TurnOffText();
                CurrentDialogueIndex = 0;
                CanTalk = false; // Prevents immediate retriggering
            }
        }
    }

    private float TimePerChar(string text)
    {
        return dialogueTime / text.Length; // Time per char to appear when the NPC is talking
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
