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
    public float KnockBackForce;
    public float UpwardKnockBackForce;

    public Rigidbody Rigidbody;

    public float TimeTillNotKinimatc;
    public float TillNotKinimatc;
    public void Awake()
    {
        GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == GC.Player && GC.TimeTillDamageAgain <= 0 && gameObject.tag == "HurtBox" && HeadDetection.isSquashed == false)
        {
            // Apply damage to the player
            GC.PlayerHP = GC.TakeDamage(Stats.Attack, GC.PlayerHP, GC.Player, Particle_Death);
            GC.TimeTillDamageAgain = GC.hurt_Time_Default;

            // Change the player's material
            MatChange(NewMat);

            // Play hurt sound for player
            if (GameController.instance.HurtSoundForPlayer != null)
            {
                GameController.instance.HurtSoundForPlayer.Play();
            }

            // Calculate knockback direction
            Vector3 knockbackDirection = (GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().orientation.forward * -1 * KnockBackForce + Vector3.up * UpwardKnockBackForce);

            if( Rigidbody != null )
            {
                Rigidbody.isKinematic = true;

                TimeTillNotKinimatc = TillNotKinimatc;
            }
            // Apply knockback to the player
            GC.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().ApplyKnockback(knockbackDirection);
            
        }
    }

    private void Update()
    {
        if(TimeTillNotKinimatc <= TillNotKinimatc )
        {
            TimeTillNotKinimatc += Time.deltaTime;
        }
        else if (TimeTillNotKinimatc >= TillNotKinimatc && Rigidbody.isKinematic == true)
        {
            if (Rigidbody != null)
            {
                Rigidbody.isKinematic = false;
            }
        }

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
