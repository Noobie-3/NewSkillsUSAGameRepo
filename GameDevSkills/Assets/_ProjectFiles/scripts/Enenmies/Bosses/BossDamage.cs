using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    private Vector3 KockBackAmount;
    float Disance;

    public float KnockBackForce;
    public bool isBeingKockedBack;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameController.instance.Player)
        {


                Disance = Vector3.Distance(GameController.instance.Player.transform.position, transform.position);


                isBeingKockedBack = true;
            
        }
    }

    private void Update()
    {
        if(isBeingKockedBack)
        {
            Vector3 knockbackDirection = (GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().orientation.forward * -1 * KnockBackForce);

            GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().ApplyKnockback(knockbackDirection);

            Disance = Vector3.Distance(GameController.instance.Player.transform.position, transform.position);
            if(Disance > 15)
            {
                isBeingKockedBack = false;
            }
        }
    }
}
