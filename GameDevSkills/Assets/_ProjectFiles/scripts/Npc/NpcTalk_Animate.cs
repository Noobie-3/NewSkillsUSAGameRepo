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
                //gameObject.SetActive(false);
            }

        }
    }

    private IEnumerator Animate()
    {

           // GameObject PrevAnim = null;

/*        for (int i = 0; i < npcAnimations.Length; i++)
        {
            npcAnimations[i].SetActive(true);

                if (npcAnimations[npcAnimations.Length -1].activeSelf == true && npcAnimations[i] != npcAnimations[npcAnimations.Length -1])
                {
                    npcAnimations[npcAnimations.Length -1].SetActive(false);
                }

                if (PrevAnim != null)
            {
                PrevAnim.SetActive(false);
                print(PrevAnim.name + "PrevAnim");

            }
            PrevAnim = npcAnimations[i];
            print(i + " TestI");
            yield return new WaitForSeconds(BetweenTimeForFrames);

            if (i == npcAnimations.Length - 1)
            {

 *//*               if (npcAnimations[i].activeSelf == true)
                {
                    npcAnimations[i].SetActive(false);
                    print(npcAnimations[i].name + "Name of npcAnnim");
                }*//*
                LoopOn = true;
                PrevAnim = npcAnimations[i];
            }

            //figure out why it only works onces LOL
            //I GOT IT YAYYYYYYYYYYYYY



        }
*/

        for (int i = 0; i < npcAnimations.Length; i++)
        { 
            image.sprite = npcAnimations[i].sprite;
            yield return new WaitForSeconds(BetweenTimeForFrames);
        }

        LoopOn = true;



    }

    private void ResetAnimState()
    {
/*        foreach (GameObject anim in npcAnimations)
        {
            anim.SetActive(false);
        }*/
        LoopOn = true;
    }
}
