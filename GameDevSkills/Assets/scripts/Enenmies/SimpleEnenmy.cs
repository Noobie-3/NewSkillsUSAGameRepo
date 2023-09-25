using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnenmy : TimeControlled
{
    public float moveSpeed = 3.0f; // Adjust this to control the speed of the enemy.
    public Transform target; // The player's transform.
    GameController GC;
    public float damage = 2;

    public float min = 2f;
    public float max = 3f;
    public bool isCharging;
    // Use this for initialization


    // Update is called once per frame


    private void Awake() {
        type = Type.Enenmy;
    }
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
        target = GC.Player.transform;
        min = transform.position.x;
        max = transform.position.x + 3;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCharging = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isCharging = false;
    }




    public void Update()
    {
        if (rb == null | animator == null | GC == null | target == null)
        {
            rb = gameObject.GetComponent<Rigidbody>();
            animator = gameObject.GetComponent<Animator>();
            GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
            target = GC.Player.transform;

        }
        if(isCharging && target != null)
        {
                // Calculate the direction from the enemy to the player.
                Vector3 moveDirection = (target.position - transform.position).normalized;

                // Calculate the new position for the enemy.
                Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

                // Move the enemy towards the player.
                transform.position = newPosition;

                transform.LookAt(target);
            
        }
        else if(!isCharging)
        {
            transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);
        }




        // Check if the player's transform (target) is not null.

    }
}

