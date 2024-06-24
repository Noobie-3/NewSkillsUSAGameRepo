using GameDevSkills;
using System;
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
    public GameObject mesh;
    public GameObject InventoryPrefab;
    /*    public Color Text_color;
        public Color Alt_Text_color;*/

    private void Start()

    { if (gameObject.GetComponent<MeshRenderer>() != null)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;

            InventorySystem = InventorySystem.Instance;

            if (GameObject.FindWithTag("Pop_UpText"))
            {
                TextMesh_Obj = GameObject.FindWithTag("Pop_UpText");
                TextMesh_Obj.SetActive(false);
            }



        }

    }

    private void Update()
    {
        if (TextMesh_Obj == null)
        {

            TextMesh_Obj = GameController.instance.PopUpTextRef.gameObject;



        }
        
        if (HasCollectedItem && GameController.instance.IsPaused == false)
        {
            mesh.SetActive(false);
            Timer_For_Text -= Time.deltaTime;
            if (TextMesh_Obj != null)
            {
                TextMesh_Obj.GetComponent<TextMeshProUGUI>().text = Alt_Text_Display;

                if (Timer_For_Text <= 0)//Destory after set time

                {
                    TextMesh_Obj.gameObject.SetActive(false);
                    Destroy(gameObject.transform.parent.gameObject);
                }
            }





        }

        }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player_01" && !HasCollectedItem)
        {


            if (TextMesh_Obj != null)
            {
                TextMesh_Obj.GetComponent<TextMeshProUGUI>().text = WhatTextToDisplay;
            
                TextMesh_Obj.SetActive(true);
            }
            if (Input.GetKeyDown(Interact_Key) && GameController.instance.IsPaused == false)
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
            }
        }
    }
    private void OnTriggerExit(Collider other)

    {
        if(TextMesh_Obj != null)
        {
            TextMesh_Obj.SetActive(false);
        }

    }
    private void FixedUpdate()
    {
        if (InventorySystem.Instance == null && InventoryPrefab != null)
        {
            Instantiate(InventoryPrefab);

        }
        InventorySystem = InventorySystem.Instance;
    }



}
