using Cinemachine;
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
    public float HP = 1;
    public NewThirdPerson NTP;

    HeadDetection HeadHit;
    // Use this for initialization


    // Update is called once per frame


    private void Awake() {
        type = Type.Enenmy;
    }
    private void Start()
    {
        if (gameObject.GetComponent<Rigidbody>() is not null)
        {
            rb = gameObject.GetComponent<Rigidbody>();

        }
        if (gameObject.GetComponent<Animator>() is not null)
        {
            animator = gameObject.GetComponent<Animator>();

        }
        if(GameObject.FindWithTag("GC").GetComponent<GameController>() is not null)
        {
            GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
        }
        min = transform.position.x;
        max = transform.position.x + 3;
        if (gameObject.GetComponentInChildren<HeadDetection>() is not null)
        {
            HeadHit = gameObject.GetComponentInChildren<HeadDetection>();
        }
        if (GameObject.FindWithTag("Player_01").TryGetComponent<NewThirdPerson>(out NTP))
        {
            target = NTP.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {if (GC.isDead == false)
        {


            if (other.gameObject.CompareTag("Player_01"))
            {
                isCharging = true;
                animator.SetBool("IsCharging", true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isCharging = false;
        animator.SetBool("IsCharging", false);

    }




    public void Update()
    {

        if (NTP != null && GC.isDead == false)

        {


            if (!NTP.isrewinding)
            {


                if (isCharging && target != null)
                {
                    if (HeadHit.isSquashed == false)
                    {


                        // Calculate the direction from the enemy to the player.
                        Vector3 moveDirection = (target.position - transform.position).normalized;
                        Vector3 LookDirection = new Vector3(target.position.x, transform.position.y, target.position.z);

                        // Calculate the new position for the enemy.
                        Vector3 newPosition = transform.position + (moveSpeed * Time.deltaTime * new Vector3(moveDirection.x, 0, moveDirection.z));

                        // Move the enemy towards the player.
                        transform.position = newPosition;

                        transform.LookAt(LookDirection);
                    }
                }
            }
        }
        else if (GC.isDead)
        {
            animator.speed = 0; 


        }
        
        /*else if(!isCharging)
        {
            transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);
        }*/




        // Check if the player's transform (target) is not null.

    }
}

