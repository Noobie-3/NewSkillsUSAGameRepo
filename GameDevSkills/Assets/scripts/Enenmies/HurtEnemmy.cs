using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemmy : MonoBehaviour
{ public GameController GC;
    public Enemy_Stats Stats;
    public  float AttackCoolDown;
    public float DefaultAttackCoolDown;
    public GameObject Particle_Death;
    public HeadDetection HeadDetection;
    public void Awake()
    {
        GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
        Stats = gameObject.transform.root.GetComponent<Enemy_Stats>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GC.Player && AttackCoolDown <= 0 && gameObject.tag == "HurtBox" && HeadDetection.isSquashed == false )
        {
           GC.PlayerHP = GC.TakeDamage(Stats.Attack, GC.PlayerHP, GC.Player, Particle_Death);
            AttackCoolDown = DefaultAttackCoolDown;
        }
    }

    private void Update()
    {
        if( AttackCoolDown >= 0 )
        {
            AttackCoolDown -= Time.deltaTime;
        }
    }
}
