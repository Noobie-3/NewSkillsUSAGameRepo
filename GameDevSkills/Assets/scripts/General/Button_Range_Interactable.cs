
using Den.Tools;
using GameDevSkills;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Range_Interactable : MonoBehaviour
{
    public float Length;
    public float DefaultLength;
    public bool IsPressed;
    [SerializeField] private bool CanBePressed = true;
    [SerializeField] private KeyCode interact_Key;
    /*RequireComponent*/
    public GameObject TextMesh_Obj;
    public bool Requires_Object;
    public GameObject Required_Object;
    [SerializeField] private Animator[] animators;
    [SerializeField] private string WhatTextToDisplay;
    [SerializeField] private string WhatTextToDisplay_OptionON;
    [SerializeField] private string WhatTextToDisplay_OptionOFF;
    [SerializeField] private string WhatTextToDisplay_OptionLOCKED;
    [SerializeField] private bool isOn;
    [SerializeField] private NewThirdPerson ntp;
    public InventorySystem InventorySystem;
    public bool IsQuest;
    public int QuestID;
    [HideInInspector]
    private void Start()
    {
        TextMesh_Obj.SetActive(false);
        InventorySystem= GameObject.FindWithTag("Player_01").GetComponentInChildren<InventorySystem>();

    }
    public void BUttonPressLength()
    {
        if (Length > 0 && CanBePressed)
        {
            Length -= Time.deltaTime;
        }
        else if (Length >= 0)
        {
        }

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player_01")
        {
            if(Requires_Object && !InventorySystem.inventoryItems.Contains(Required_Object)) {
                WhatTextToDisplay = WhatTextToDisplay_OptionLOCKED;
            }
            TextMesh_Obj.GetComponent<Text>().text = WhatTextToDisplay;
            TextMesh_Obj.SetActive(true);

            if (Input.GetKey(interact_Key) && Length <= 0 && GameController.instance.IsPaused == false)
            {


                /*                print("Button Pressed ");*/

                if (Requires_Object)
                {

                    if (InventorySystem.inventoryItems.Contains(Required_Object))
                    {
                        QuestCheck();

                        if (isOn == true)
                        {
                            Length = DefaultLength;

                            isOn = false;
                            for (int i = 0; i < animators.Length; i++)
                            {
                                animators[i].SetBool("Button_Pressed", false);

                            }
                            WhatTextToDisplay = WhatTextToDisplay_OptionOFF;
                        }


                        else if (isOn == false)
                        {
                            Length = DefaultLength;

                            isOn = true;
                            for (int i = 0; i < animators.Length; i++)
                            {
                                animators[i].SetBool("Button_Pressed", true);

                            }
                            WhatTextToDisplay = WhatTextToDisplay_OptionON;
                            print("Help me");

                        }
                        InventorySystem.inventoryItems.Remove(Required_Object);
                        Requires_Object = false;
                    }
                    else
                    {
                        WhatTextToDisplay = WhatTextToDisplay_OptionLOCKED;
                    }

                }



                else if (!Requires_Object)
                {
                    QuestCheck();

                    if (isOn == true)
                    {
                        Length = DefaultLength;


                        isOn = false;
                        for (int i = 0; i < animators.Length; i++)
                        {
                            animators[i].SetBool("Button_Pressed", false);

                        }
                        WhatTextToDisplay = WhatTextToDisplay_OptionOFF;
                    }
                    else if (isOn == false)
                    {
                        Length = DefaultLength;

                        isOn = true;
                        TextMesh_Obj.SetActive(false);
                        print("should Be Off");

                        for (int i = 0; i < animators.Length; i++)
                        {
                            animators[i].SetBool("Button_Pressed", true);

                        }
                        WhatTextToDisplay = WhatTextToDisplay_OptionON;

                    }
                }
            }
        }


        
    }


    private void QuestCheck()
    {

        if (IsQuest)
        {
            QuestManager.Instance.CompleteQuest(QuestID);
            IsQuest = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        TextMesh_Obj.SetActive(false);

    }







    private void Update()
    {

        if (Length > 0)
        {
            BUttonPressLength();
        }




    }

}
