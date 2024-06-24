using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Ui_OnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameController.instance.IsPaused = false;
        

    }

    private void OnEnable()
    {
        print("ObjectEnabled");
    }

}
