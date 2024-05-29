using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Curency_show : MonoBehaviour
{
    public Text Curr_Value;
    public int Curr_ValueIndex;
    private GameController GC;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        GC = GameObject.FindWithTag("GC").GetComponent<GameController>();        
    }


    private void Update()
    {
        if(GC.Current_Currency != Curr_ValueIndex)
        {
            Animate();
            Curr_ValueIndex = GameController.instance.Current_Currency;
        }
    }
    // Update is called once per frame


    //UpdateText
    public void UpdateText()
    {
        Curr_Value.text = GameController.instance.Current_Currency.ToString();
    }

    public void Reset()
    {
        anim.SetBool("Collected", false);   

    }


    public void Animate()
    {
        anim.SetBool("Collected", true);
    }
}
