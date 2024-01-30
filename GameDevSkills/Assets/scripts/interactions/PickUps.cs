using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUps : MonoBehaviour
{
    public GameObject Item_To_Be_PickedUp;
    public GameObject TextMesh_Obj;
    public KeyCode Interact_Key;
    public InventorySystem InventorySystem;
    public string WhatTextToDisplay;
    public string Alt_Text_Display;
    public float Timer_For_Text;
    public bool HasCollectedItem;
/*    public Color Text_color;
    public Color Alt_Text_color;*/

    private void Start()
    {
        if (GameObject.FindWithTag("Pop_UpText"))
        {
            TextMesh_Obj = GameObject.FindWithTag("Pop_UpText");
            TextMesh_Obj.SetActive(false);
        }

        if (GameObject.FindWithTag("Player").GetComponentInChildren<InventorySystem>())
        {
            InventorySystem = GameObject.FindWithTag("Player").GetComponentInChildren<InventorySystem>();
        }


    }
    private void Update()
    {if(HasCollectedItem)
        {
            Timer_For_Text -= Time.deltaTime;
            TextMesh_Obj.GetComponent<Text>().text = Alt_Text_Display;
            if (Timer_For_Text <= 0 )//Destory after set time

            {
                TextMesh_Obj.gameObject.SetActive(false);
                Destroy(gameObject.transform.root.gameObject);
            }
        }
        if(TextMesh_Obj== null)
        {
            TextMesh_Obj = GameObject.FindGameObjectWithTag("Pop_UpText");
            
        }
        
        if(InventorySystem == null)
        {
            InventorySystem = GameObject.FindWithTag("Player").GetComponentInChildren<InventorySystem>();

        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !HasCollectedItem) {
            TextMesh_Obj.GetComponent<Text>().text = WhatTextToDisplay;

            TextMesh_Obj.SetActive(true);
            if(Input.GetKeyDown(Interact_Key)) {
                InventorySystem.AddItem(Item_To_Be_PickedUp);
                HasCollectedItem = true;
            }
        }

    }
    private void OnTriggerExit(Collider other)

    {
        TextMesh_Obj.SetActive(false);
    }
}
