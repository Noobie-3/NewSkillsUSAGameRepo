using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTut : MonoBehaviour
{
    public GameObject PopUp;
    public GameObject PauseScreen;
    private bool HasDoneTut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameController.instance.Player.gameObject && HasDoneTut == false)
        {
            if (PopUp is not null)
            {
                PopUp.SetActive(true);
            }
            if(PauseScreen is not null)
            {
                PauseScreen.SetActive(true);
            }
            GameController.instance.IsPaused = true;
            HasDoneTut = true;
        }
    }
}
