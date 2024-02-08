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
    GameController GC;
    NewThirdPerson ntp;
    // Update is called once per frame
    void Update()
    { if (GC != null && ntp.canRewind)
        {
            loadingProgress = (float)((GC.Player.GetComponent<TimeRewinderV2>().ntp.currentRecordingTime / GC.Player.GetComponent<TimeRewinderV2>().ntp.maxRecordingDuration));
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
        if(ntp.canRewind == false) {

            loadingImage.fillAmount = 0;

        }




    }
    private void Start()
    {
        if (GameObject.FindWithTag("GC").GetComponent<GameController>())
        {
            GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
        }
        if (GameObject.FindGameObjectWithTag("Player_01").GetComponent<NewThirdPerson>())
        {
            ntp = GameObject.FindGameObjectWithTag("Player_01").GetComponent<NewThirdPerson>();
        }


    }
}