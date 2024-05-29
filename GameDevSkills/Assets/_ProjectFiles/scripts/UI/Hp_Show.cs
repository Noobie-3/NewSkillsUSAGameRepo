using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_Show : MonoBehaviour
{
    public float Hp;

    public float MaxHp;
    public Image HpBar;
/*    public float FirstDropoff;
    public float SecondDropoff;*/

/*    public GameObject FullHp;
    public GameObject FirstDropHp;
    public GameObject SecondDropHp;*/
   // public GameController Controller;


    // Update is called once per frame
/*    void Update()
    {
*//*        Hp = Controller.PlayerHP;
        if (Hp >= MaxHp && FullHp.activeInHierarchy == false)
        {
            FullHp.SetActive(true);
            FirstDropHp.SetActive(false);
            SecondDropHp.SetActive(false);
        }
        else if (Hp == MaxHp / FirstDropoff && FirstDropHp.activeInHierarchy == false) {
            FullHp.SetActive(false);
            FirstDropHp.SetActive(true);
            SecondDropHp.SetActive(false);
        }


        else if( Hp == MaxHp / SecondDropoff && SecondDropHp.activeInHierarchy == false) {
            FullHp.SetActive(false);
            FirstDropHp.SetActive(false);
            SecondDropHp.SetActive(true);
        }*/
/*        print(Hp);
        print(MaxHp / FirstDropoff + "FirstDrop");    
        print(MaxHp / SecondDropoff + "Second");    *//*
    }*/

    private void Start()
    {
        MaxHp = GameController.instance.PlayerMaxHP;

    }


    private void Update()
    {
        if(Hp != GameController.instance.PlayerHP)
        {

            //lerp hp down to to gameControllers hp to make bar look smooth
            Hp = Mathf.Lerp(Hp, GameController.instance.PlayerHP, Time.deltaTime * 2);

            HpBar.fillAmount = Hp / MaxHp;

        }
    }

}
