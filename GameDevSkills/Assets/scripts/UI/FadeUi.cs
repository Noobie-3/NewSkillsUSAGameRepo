using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeUi : MonoBehaviour
{
    public float TimeTillFade;
    public float TimeTillFadeDefault;
    public int fadeAmount;
    public GameObject TextToFade;
    public int NewFadeAmmount;
    // Start is called before the first frame update
    void Start()
    {
        NewFadeAmmount = 255;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeTillFade > 0)
        {
            TimeTillFade-= 1 * Time.deltaTime;
        }
        else
        {
            NewFadeAmmount -= fadeAmount;
            TextToFade.GetComponent<TextMeshProUGUI>().color = new Color(TextToFade.GetComponent<TextMeshProUGUI>().color.r, TextToFade.GetComponent<TextMeshProUGUI>().color.g, TextToFade.GetComponent<TextMeshProUGUI>().color.b, NewFadeAmmount);
            TimeTillFade = TimeTillFadeDefault;
        }

    }
}
