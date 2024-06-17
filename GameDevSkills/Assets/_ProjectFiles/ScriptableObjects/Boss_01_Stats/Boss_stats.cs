using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Boss_Stats", menuName = "Boss/Boss_BossStats")]

public class Boss_stats : ScriptableObject
{

    public int BHp_Default;
    public float Attack;
    public float RangeAttack;
    public GameObject KillPartcle;
    public int currencyGiven;
    public string Name;
    [Range(0, 100)]
    public float ChanceToHeal;
    public AudioClip HurtSound;
    public AudioClip DeathSound;
    public AudioClip BossMusic;
    public List<GameObject> prefabList;
}



