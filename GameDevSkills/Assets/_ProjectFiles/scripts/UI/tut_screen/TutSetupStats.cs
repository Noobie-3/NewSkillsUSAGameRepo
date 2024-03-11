using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutSetupStats : MonoBehaviour
{
    [SerializeField]
    public TutScreen CurrentStats_SO;
    [SerializeField]
    private TextMeshProUGUI TextTut;
    [SerializeField]
    private VideoPlayer VideoTut;
    private void OnEnable()
    {
        if(CurrentStats_SO is not null)
        {
            if(TextTut.text != CurrentStats_SO.TextForTut)
            {
                TextTut.text = CurrentStats_SO.TextForTut;
                print(TextTut.text);
            }

            if(VideoTut.clip != CurrentStats_SO.VideoForTut)
            {
                VideoTut.clip = CurrentStats_SO.VideoForTut;
            }

        }
    }
}
