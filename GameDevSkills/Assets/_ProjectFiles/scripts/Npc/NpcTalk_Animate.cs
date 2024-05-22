using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcTalk_Animate : MonoBehaviour
{

    public Npc_Basic npc_Basic;
    public static NpcTalk_Animate instance;
    public Image[] npcAnimations;
    public bool LoopOn = true;
    public float BetweenTimeForFrames;
    public TextMeshProUGUI text;
    public Image image;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        
    }

    private void Update()
    {
        if(npc_Basic != null )
        {
            if(npc_Basic.isTalking && LoopOn == true)
            {
                LoopOn = false;

                StartCoroutine(Animate());
            }
            else if (npc_Basic.isTalking == false)
            {
                print("Turned Object off");
                ResetAnimState();
                if (Input.anyKey)
                {
                    gameObject.SetActive(false);

                }
            }

        }
    }

    private IEnumerator Animate()
    {

        for (int i = 0; i < npcAnimations.Length; i++)
        { 
            image.sprite = npcAnimations[i].sprite;
            yield return new WaitForSeconds(BetweenTimeForFrames);
        }

        LoopOn = true;



    }

    private void ResetAnimState()
    {
        LoopOn = true;
    }
}
