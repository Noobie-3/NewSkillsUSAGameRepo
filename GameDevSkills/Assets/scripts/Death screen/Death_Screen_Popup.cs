using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death_Screen_Popup : MonoBehaviour
{
    public GameController Gc;
    public GameObject DeathScreen;
    public GameObject BaseScreen;
    public string RetrySceneName;
    // Start is called before the first frame update
    void Start()
    {
        Gc = GameObject.FindWithTag("GC").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Gc.isDead && BaseScreen.activeSelf == true)
        {
            BaseScreen.SetActive(false);
            DeathScreen.SetActive(true);
            if(Input.GetKeyUp(KeyCode.E)) {

                SceneManager.LoadScene(RetrySceneName);
            }
        }

    }
}
