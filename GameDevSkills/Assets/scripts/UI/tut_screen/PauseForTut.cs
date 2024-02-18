using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseForTut : MonoBehaviour
{
    public GameObject Ui_Screen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            GameController.instance.IsPaused = true;
            Ui_Screen.SetActive(false);
        }
    }
}
