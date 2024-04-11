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
    private Material[] Materials;
    public Material OldMat;
    public Material NewMat;
    public float FlashTime;

    public void Awake()
    {
        GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == GC.Player && GC.TimeTillDamageAgain <= 0 && gameObject.tag == "HurtBox" && HeadDetection.isSquashed == false )
        {
           GC.PlayerHP = GC.TakeDamage(Stats.Attack, GC.PlayerHP, GC.Player, Particle_Death);
            GC.TimeTillDamageAgain = GC.hurt_Time_Default;

            MatChange(NewMat);

            if(GameController.instance.HurtSoundForPlayer != null)
            {
                GameController.instance.HurtSoundForPlayer.Play();
            }
        }
    }

    private void Update()
    {
        if(Stats == null )
        {
            Stats = gameObject.transform.root.GetComponent<Enemy_Stats>();
        }
        if(GC == null )
        {
            GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
        }
        if ( AttackCoolDown >= 0 )
        {
            AttackCoolDown -= Time.deltaTime;
        }


    }

    private void MatChange(Material Mat)
    {
        Materials = GameController.instance.Player.gameObject.GetComponentInChildren<Renderer>().materials;
        if(Materials !=  null )
        {
            for (int i = 0; i < Materials.Length; i++)
            {
                Materials[i] = Mat;
                print(Materials[i].name + "" + i + "   " +  Materials.Length);
            }
            GameController.instance.Player.gameObject.GetComponentInChildren<Renderer>().materials = Materials;
        }

    }
}
