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
<<<<<<< HEAD
    { if (GC != null && ntp.canRewind)
        {
            loadingProgress = (float)((GC.Player.GetComponent<TimeRewinderV2>().ntp.currentRecordingTime / GC.Player.GetComponent<TimeRewinderV2>().ntp.maxRecordingDuration));
            loadingImage.fillAmount = loadingProgress;
        }
=======
    {
        loadingProgress = (float)((GC.Player.GetComponent<TimeRewinderV2>().ntp.currentRecordingTime / GC.Player.GetComponent<TimeRewinderV2>().ntp.maxRecordingDuration));
        loadingImage.fillAmount = loadingProgress;
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
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
<<<<<<< HEAD
        if (GameObject.FindWithTag("GC").GetComponent<GameController>())
        {
            GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
        }
        if (GameObject.FindGameObjectWithTag("Player_01").GetComponent<NewThirdPerson>())
        {
            ntp = GameObject.FindGameObjectWithTag("Player_01").GetComponent<NewThirdPerson>();
        }
=======
        GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
        ntp = GameObject.FindGameObjectWithTag("Player").GetComponent<NewThirdPerson>();
<<<<<<< HEAD
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
=======
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e

    }
}