using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Bullet_Behavior : MonoBehaviour
{
    public GameObject Enemy;
    public Boss_stats stats;

    public float TimetillDeath;
    public float KnockBackForce = 10;
    public float  UpwardKnockBackForce = 15;


    private void Start()
    {
        Destroy(gameObject, TimetillDeath);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == GameController.instance.Player)
        {
            if (GameController.instance.TimeTillDamageAgain <= 0)
            {
                // Apply damage to the player
                GameController.instance.PlayerHP = GameController.instance.TakeDamage(stats.RangeAttack, GameController.instance.PlayerHP, GameController.instance.Player);
                GameController.instance.TimeTillDamageAgain = GameController.instance.hurt_Time_Default;

                GameController.instance.MatChange(GameController.instance.HurtMat, GameController.instance.Player);

                // Play hurt sound for player
                if (GameController.instance.HurtSoundForPlayer != null)
                {
                    GameController.instance.HurtSoundForPlayer.Play();
                }


            }
            // Calculate knockback direction
            Vector3 knockbackDirection = (GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().orientation.forward * -1 * KnockBackForce + Vector3.up * UpwardKnockBackForce);

            // Apply knockback to the player

            GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().ApplyKnockback(knockbackDirection);

            
            Destroy(gameObject);
        }
    }

}
