using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUps : MonoBehaviour
{
    public GameObject Item_To_Be_PickedUp;
<<<<<<< HEAD
<<<<<<< HEAD
    public GameObject Item_To_Be_PickedUp_Instan;
=======
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
=======
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
    public GameObject TextMesh_Obj;
    public KeyCode Interact_Key;
    public InventorySystem InventorySystem;
    public string WhatTextToDisplay;
    public string Alt_Text_Display;
    public float Timer_For_Text;
    public bool HasCollectedItem;
<<<<<<< HEAD
<<<<<<< HEAD
    public GameObject Partical;
    public AudioSource Collection_Effect;
    public bool IsQuest;
    public questmanager QM;
=======
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
=======
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
/*    public Color Text_color;
    public Color Alt_Text_color;*/

    private void Start()
<<<<<<< HEAD
<<<<<<< HEAD
    {if(gameObject.GetComponent<MeshRenderer>() != null)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
=======
    {
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
=======
    {
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
        if (GameObject.FindWithTag("Pop_UpText"))
        {
            TextMesh_Obj = GameObject.FindWithTag("Pop_UpText");
            TextMesh_Obj.SetActive(false);
        }

<<<<<<< HEAD
<<<<<<< HEAD
        if (GameObject.FindWithTag("Player_01").GetComponentInChildren<InventorySystem>())
        {
            InventorySystem = GameObject.FindWithTag("Player_01").GetComponentInChildren<InventorySystem>();
=======
        if (GameObject.FindWithTag("Player").GetComponentInChildren<InventorySystem>())
        {
            InventorySystem = GameObject.FindWithTag("Player").GetComponentInChildren<InventorySystem>();
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
=======
        if (GameObject.FindWithTag("Player").GetComponentInChildren<InventorySystem>())
        {
            InventorySystem = GameObject.FindWithTag("Player").GetComponentInChildren<InventorySystem>();
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
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
<<<<<<< HEAD
<<<<<<< HEAD
            if (GameObject.FindWithTag("Pop_UpText"))
            {
                TextMesh_Obj = GameObject.FindWithTag("Pop_UpText");
            }

=======
            TextMesh_Obj = GameObject.FindGameObjectWithTag("Pop_UpText");
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
=======
            TextMesh_Obj = GameObject.FindGameObjectWithTag("Pop_UpText");
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
            
        }
        
        if(InventorySystem == null)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            if (GameObject.FindWithTag("Player_01").GetComponentInChildren<InventorySystem>())
            {
                InventorySystem = GameObject.FindWithTag("Player_01").GetComponentInChildren<InventorySystem>();
            }
=======
            InventorySystem = GameObject.FindWithTag("Player").GetComponentInChildren<InventorySystem>();

>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
=======
            InventorySystem = GameObject.FindWithTag("Player").GetComponentInChildren<InventorySystem>();

>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
        }

    }
    private void OnTriggerStay(Collider other)
    {
<<<<<<< HEAD
<<<<<<< HEAD
        if (other.gameObject.tag == "Player_01" && !HasCollectedItem) {
=======
        if (other.gameObject.tag == "Player" && !HasCollectedItem) {
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
=======
        if (other.gameObject.tag == "Player" && !HasCollectedItem) {
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
            TextMesh_Obj.GetComponent<Text>().text = WhatTextToDisplay;

            TextMesh_Obj.SetActive(true);
            if(Input.GetKeyDown(Interact_Key)) {
<<<<<<< HEAD
<<<<<<< HEAD
                if(IsQuest)
                {
                    QM.OnComplete();
                }
                InventorySystem.AddItem(Item_To_Be_PickedUp);
                HasCollectedItem = true;
                Instantiate(Partical, transform.position, Quaternion.identity);
                if(Collection_Effect != null)
                {
                    Collection_Effect.Play();
                }
=======
                InventorySystem.AddItem(Item_To_Be_PickedUp);
                HasCollectedItem = true;
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
=======
                InventorySystem.AddItem(Item_To_Be_PickedUp);
                HasCollectedItem = true;
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
            }
        }

    }
    private void OnTriggerExit(Collider other)

    {
        TextMesh_Obj.SetActive(false);
    }
}
