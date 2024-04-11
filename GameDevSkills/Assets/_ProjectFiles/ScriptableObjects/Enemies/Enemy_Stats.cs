using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy_Stats", menuName = "Enemy/Enemy_Stats")]
public class Enemy_Stats : ScriptableObject
{

    public int EHp_Default;
    public float Attack;
    public GameObject KillPartcle;
    public int currencyGiven;
    [Range(0, 100)]
    public float ChanceToHeal;
    public GameObject HealingItem;
    public AudioClip HurtSound;
}
