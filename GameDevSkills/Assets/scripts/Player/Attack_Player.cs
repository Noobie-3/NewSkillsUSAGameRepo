using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Player : MonoBehaviour
{
    // Start is called before the first frame update

    GameController GC;


    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponentInParent<TimeRewinderV2>().Isrewinding)
        {
            
        }


    }
    private void OnTriggerStay(Collider other)
    {
        
        SimpleEnenmy enemy = other.GetComponentInParent<SimpleEnenmy>();

        //  damage  enemy
         enemy.HP = GC.TakeDamage(GC.PlayerMight, enemy.HP, enemy.gameObject);


    }
}

