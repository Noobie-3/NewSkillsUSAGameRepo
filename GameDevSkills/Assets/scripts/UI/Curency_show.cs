using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Curency_show : MonoBehaviour
{
    public Text Curr_Value;
    public int Curr_ValueIndex;
    private GameController GC;
    // Start is called before the first frame update
    void Start()
    {
        GC = GameObject.FindWithTag("GC").GetComponent<GameController>();        
        Curr_Value = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Curr_Value.text = GC.Current_Currency.ToString();
    }
}
