using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry_Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.E)) {

            this.gameObject.SetActive(false);
            GameController.instance.ResetValues();
            Death_Screen_Popup.instance.DeathScreen.SetActive(false);
            Death_Screen_Popup.instance.BaseScreen.SetActive(true);
            GameController.instance.BossBar.gameObject.SetActive(false);
            NpcTalk_Animate.instance.gameObject.SetActive(false);
            if(GameObject.FindWithTag("TutScreen") != null)
            {
                GameObject.FindWithTag("TutScreen").SetActive(false);

            }
            GameStateManager.Instance.newSceneInfo.newPoition = GameController.instance.LevelStartPositions[0];
            SceneManager.LoadScene("Level_01_Factory");

        }
    }
}
