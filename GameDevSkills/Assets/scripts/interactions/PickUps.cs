using GameDevSkills;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUps : MonoBehaviour
{
    public GameObject Item_To_Be_PickedUp;

    public GameObject Item_To_Be_PickedUp_Instan;

    public GameObject TextMesh_Obj;
    public KeyCode Interact_Key;
    public InventorySystem InventorySystem;
    public string WhatTextToDisplay;
    public string Alt_Text_Display;
    public float Timer_For_Text;
    public bool HasCollectedItem;

    public GameObject Partical;
    public AudioSource Collection_Effect;
    public bool IsQuest;
    public int QuestID;
    /*    public Color Text_color;
        public Color Alt_Text_color;*/

    private void Start()

    { if (gameObject.GetComponent<MeshRenderer>() != null)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;



            if (GameObject.FindWithTag("Pop_UpText"))
            {
                TextMesh_Obj = GameObject.FindWithTag("Pop_UpText");
                TextMesh_Obj.SetActive(false);
            }

            if (GameObject.FindWithTag("Player_01").GetComponentInChildren<InventorySystem>())
            {
                InventorySystem = GameObject.FindWithTag("Player_01").GetComponentInChildren<InventorySystem>();
                if (GameObject.FindWithTag("Player").GetComponentInChildren<InventorySystem>())
                {
                    InventorySystem = GameObject.FindWithTag("Player").GetComponentInChildren<InventorySystem>();
                    if (GameObject.FindWithTag("Player").GetComponentInChildren<InventorySystem>())
                    {
                        InventorySystem = GameObject.FindWithTag("Player").GetComponentInChildren<InventorySystem>();
                    }


                }
            }
        }
    }

    private void Update()
    { if (HasCollectedItem)
        {
            Timer_For_Text -= Time.deltaTime;
            TextMesh_Obj.GetComponent<Text>().text = Alt_Text_Display;
            if (Timer_For_Text <= 0)//Destory after set time

            {
                TextMesh_Obj.gameObject.SetActive(false);
                Destroy(gameObject.transform.parent.gameObject);
            }

            if (TextMesh_Obj == null)
            {
                if (GameObject.FindWithTag("Pop_UpText"))
                {
                    TextMesh_Obj = GameObject.FindWithTag("Pop_UpText");
                }

                TextMesh_Obj = GameObject.FindGameObjectWithTag("Pop_UpText");
                TextMesh_Obj = GameObject.FindGameObjectWithTag("Pop_UpText");

            }

            if (InventorySystem == null)
            {
                if (GameObject.FindWithTag("Player_01").GetComponentInChildren<InventorySystem>())
                {
                    InventorySystem = GameObject.FindWithTag("Player_01").GetComponentInChildren<InventorySystem>();
                }
                InventorySystem = GameObject.FindWithTag("Player").GetComponentInChildren<InventorySystem>();

                InventorySystem = GameObject.FindWithTag("Player").GetComponentInChildren<InventorySystem>();

            }
        }

        }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player_01" && !HasCollectedItem) {
                    TextMesh_Obj.GetComponent<Text>().text = WhatTextToDisplay;

                    TextMesh_Obj.SetActive(true);
            if (Input.GetKeyDown(Interact_Key))
            {
                if (IsQuest)
                {
;                    QuestManager.Instance.CompleteQuest(QuestID);
                }
                InventorySystem.AddItem(Item_To_Be_PickedUp);
                HasCollectedItem = true;
                Instantiate(Partical, transform.position, Quaternion.identity);
                if (Collection_Effect != null)
                {
                    Collection_Effect.Play();
                }
                InventorySystem.AddItem(Item_To_Be_PickedUp);
                HasCollectedItem = true;
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
