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
    // Update is called once per frame
    void Update()
    {
        loadingProgress = (float)((GC.Player.GetComponent<TimeRewinderV2>().currentRecoringTime / GC.Player.GetComponent<TimeRewinderV2>().maxRecordingDuration));
        loadingImage.fillAmount = loadingProgress;
        if (loadingProgress < 1)
        {
            //   loadingText.text = Mathf.RoundToInt(loadingProgress * 100) + "%\nLoading...";
        }
        else
        {
            // loadingText.text = "Done.";
        }
    }
    private void Start()
    {
        GC = GameObject.FindWithTag("GC").GetComponent<GameController>();


    }
}