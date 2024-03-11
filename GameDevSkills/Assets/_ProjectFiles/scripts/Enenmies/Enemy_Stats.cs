using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : MonoBehaviour
{

    public int EHp;
    public float Attack;
    public GameObject KillPartcle;
    public int currencyGiven;
    public GameController GC;
    [Range(0, 100)]
    public float ChanceToHeal;
    public float RandomNumber;
    public GameObject HealingItem;


    // Start is called before the first frame update
    void Start()
    {
        GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(EHp <= 0)
        {
            Death();
        }
    }

    public void ParticleEffect(Transform Enemy)
    {
        Instantiate(KillPartcle, Enemy.position, Enemy.rotation);
    }

    public void Death() {
        GC.GainCurrency(currencyGiven);
        ParticleEffect(gameObject.transform);
        RandomNumber = UnityEngine.Random.Range(1, 100);
        if(RandomNumber <= ChanceToHeal && HealingItem != null) {
        Instantiate(HealingItem, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
