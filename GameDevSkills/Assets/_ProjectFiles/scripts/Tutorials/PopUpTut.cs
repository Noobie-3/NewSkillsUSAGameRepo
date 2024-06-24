using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTut : MonoBehaviour
{
    public GameObject PopUp;
    public GameObject PauseScreen;
    private bool HasDoneTut;
    public TutScreen TutData;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(PauseScreen == null)
        {
            PauseScreen = GameController.instance.TutScreen.gameObject;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameController.instance.Player.gameObject && HasDoneTut == false)
        {
            SetActive();
            GameController.instance.IsPaused = true;
            HasDoneTut = true;
        }
    }

    private void SetActive()
    {
        if(HasDoneTut == false)
        {

            if (PauseScreen is not null )
            {
                PauseScreen.GetComponent<TutSetupStats>().CurrentStats_SO = TutData;
                PauseScreen.gameObject.SetActive(true);
            }

            if (PopUp != null)
            {
                PopUp.SetActive(true);
            }
        }
    }
}
