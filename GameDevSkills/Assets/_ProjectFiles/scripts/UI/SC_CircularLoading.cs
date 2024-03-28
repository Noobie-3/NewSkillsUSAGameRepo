using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_CircularLoading : MonoBehaviour
{
    public Image loadingImage;
    public Text loadingText;
    [Range(0, 1)]
    public float loadingProgress = 0;
    // Update is called once per frame
    void Update()
    { if (GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().canRewind)
        {
            loadingProgress = (float)((GameController.instance.Player.gameObject.GetComponent<REVAMPEDPLAYERCONTROLLER>().currentRecordingTime / GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().maxRecordingDuration));
            loadingImage.fillAmount = loadingProgress;
        }
        if (loadingProgress < 1)
        {
            //   loadingText.text = Mathf.RoundToInt(loadingProgress * 100) + "%\nLoading...";
        }
        else
        {
            // loadingText.text = "Done.";

        }
        if(GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().canRewind == false) {

            loadingImage.fillAmount = 0;

        }




    }
    private void Start()
    {




    }
}