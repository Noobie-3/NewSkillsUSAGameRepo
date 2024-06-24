using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death_Screen_Popup : MonoBehaviour
{
    public GameObject DeathScreen;
    public GameObject BaseScreen;
    public string RetrySceneName;
    public  static Death_Screen_Popup instance;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;

        }

        else
        {
            Destroy(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.instance.isDead && BaseScreen.activeSelf == true)
        {
            BaseScreen.SetActive(false);
            DeathScreen.SetActive(true);
            if(Input.GetKeyUp(KeyCode.E)) {

                SceneManager.LoadScene(RetrySceneName);

            }
        }

    }
}
