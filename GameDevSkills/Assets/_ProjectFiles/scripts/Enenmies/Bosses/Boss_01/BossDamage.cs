using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    private Vector3 KockBackAmount;
    float Disance;
    public Boss_StateMachine boss;
    public float KnockBackForce;
    public bool isBeingKockedBack;
    public Vector3 knockbackDirection;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameController.instance.Player && gameObject.tag == "HurtBox")
        {
            if(!boss.IsInvulnerable)
            {
                boss.IsInvulnerable = true;
                boss.InInvulnerableTime = boss.InInvulnerableTimeDefault;
                boss.health --;
                print(boss.health);
                boss.AllSpawned = false;
                isBeingKockedBack = true;
                boss.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                knockbackDirection = (transform.position - GameController.instance.Player.transform.position * KnockBackForce);
                knockbackDirection.y = 0;
                knockbackDirection.Normalize();
            }

        }
    }

    private void Update()
    {
        if (isBeingKockedBack)
        {


            GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().ApplyKnockback(knockbackDirection * KnockBackForce);

            Disance = Vector3.Distance(GameController.instance.Player.transform.position, transform.position);
            if (Disance > 15)
            {
                isBeingKockedBack = false;
                GameController.instance.Player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0 );
            }
        }
        else if (
        !isBeingKockedBack)
        {
            if(boss.gameObject.GetComponent<Rigidbody>().isKinematic == true)
            {
                boss.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
    private void Start()
    {
        if(GetComponent<Boss_StateMachine>() != null)
        {
            boss = GetComponent<Boss_StateMachine>();
        }
    }
}
