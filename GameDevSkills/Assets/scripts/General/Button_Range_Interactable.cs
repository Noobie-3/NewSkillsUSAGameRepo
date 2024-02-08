
using Den.Tools;
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
    [SerializeField] private bool isOn;
    [SerializeField] private NewThirdPerson ntp;
    [HideInInspector]
    private void Start()
    {
        TextMesh_Obj.GetComponent<Text>().text = WhatTextToDisplay;
        TextMesh_Obj.SetActive(false);
        InventorySystem= GameObject.FindWithTag("Player").GetComponentInChildren<InventorySystem>();
    }
    public void BUttonPressLength()
    {
        if (Length > 0 && CanBePressed)
        {
            Length -= Time.deltaTime;
            IsPressed = true;
        }
        else if (Length >= 0)
        {
            IsPressed = false;
        }

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            TextMesh_Obj.GetComponent<Text>().text = WhatTextToDisplay;                             
            TextMesh_Obj.SetActive(true);

            if (Input.GetKey(interact_Key) && Length <= 0)
            {
                Length = DefaultLength;
                print("Button Pressed ");

                if (Requires_Object)
                {
                    if (isOn == true)
                    {
                        isOn = false;
                        for (int i = 0; i < animators.Length; i++)
                        {
                            animators[i].SetBool("Button_Pressed", false);

                        }
                        WhatTextToDisplay = WhatTextToDisplay_OptionOFF;
                        print("Help me");
                    }
                    else if (isOn == false)
                    {
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
                }

                }

                else
            if (isOn == true)
                {

                isOn = false;
                for (int i = 0; i < animators.Length; i++)
                {
                    animators[i].SetBool("Button_Pressed", false);

                }
                WhatTextToDisplay = WhatTextToDisplay_OptionOFF;
            }
            else if (isOn == false)
            {
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

    private void OnTriggerExit(Collider other)
    {
        TextMesh_Obj.SetActive(false);

    }







    private void Update()
    {
        TextMesh_Obj.GetComponent<Text>().text = WhatTextToDisplay;

        if (Length > 0)
        {
            BUttonPressLength();
        }




    }

}
