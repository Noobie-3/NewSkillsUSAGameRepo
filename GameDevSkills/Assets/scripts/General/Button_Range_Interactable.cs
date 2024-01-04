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
    [SerializeField] private Animator animator;
    [SerializeField] private string WhatTextToDisplay;
    [SerializeField] private string WhatTextToDisplay_OptionON;
    [SerializeField] private string WhatTextToDisplay_OptionOFF;
    private bool isOn;
    [HideInInspector]
    private void Start()
    {
        TextMesh_Obj.GetComponent<Text>().text = WhatTextToDisplay;
        TextMesh_Obj.SetActive(false);

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

        if(other.gameObject.tag == "Player") {
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
                        animator.SetBool("Button_Pressed", false);
                        WhatTextToDisplay = WhatTextToDisplay_OptionOFF;
                    }
                    else if (isOn == false)
                    {
                        isOn = true;
                        animator.SetBool("Button_Pressed", true);
                        WhatTextToDisplay = WhatTextToDisplay_OptionON;

                    }
                }
                else

    if (isOn == true)
                {
                    TextMesh_Obj.SetActive(false);

                    isOn = false;
                    animator.SetBool("Button_Pressed", false);
                    WhatTextToDisplay = WhatTextToDisplay_OptionOFF;
                }
                else if (isOn == false)
                {
                    isOn = true;
                    TextMesh_Obj.SetActive(false);
                    print("should Be Off");

                    animator.SetBool("Button_Pressed", true);
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
