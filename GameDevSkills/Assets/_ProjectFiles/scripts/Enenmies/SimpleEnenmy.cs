using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class SimpleEnenmy : MonoBehaviour 
{
    public float moveSpeed = 3.0f; // Adjust this to control the speed of the enemy.
    public Transform target; // The player's transform.
    public float damage = 1;

    public float min = 2f;
    public float max = 3f;
    public bool isCharging;
    public float HP = 1;
    public NewThirdPerson NTP;
    public Rigidbody rb;
    public Animator animator;
    public bool ISFULLMAP;
    public HeadDetection HeadHit;
    public float TargetDistanceToCharge;
    public float Distance;

    // Use this for initialization


    // Update is called once per frame


    private void Awake() {
    }
    private void Start()
    {
        if (gameObject.GetComponent<Rigidbody>() && rb == null)
            rb = gameObject.GetComponent<Rigidbody>();
        if (gameObject.GetComponent<Animator>() & animator == null)
                animator = gameObject.GetComponent<Animator>();

        min = transform.position.x;
        max = transform.position.x + 3;
        if (gameObject.GetComponentInChildren<HeadDetection>())
        {
            HeadHit = gameObject.GetComponentInChildren<HeadDetection>();
        }

    }


    
        private void OnTriggerExit(Collider other)
    {
        if(other == GameController.instance.Player)
        {
            isCharging = false;
            animator.SetBool("IsCharging", false);

        }

    }




    public void Update()
    {

        if(target == null)
        {
            if(GameController.instance.Player != null)
            {
                target = GameController.instance.Player.transform;

            }
        }
        if (GameController.instance.IsPaused == true)
        {
            animator.speed = 0;


        }
        else if (GameController.instance.IsPaused == false )

        {
            animator.speed = 1;
           
            if (!GameController.instance.Player.GetComponent<TimeRewinderV2>().Isrewinding)
            {
                if(target != null)
                {

                
                     Distance = Vector3.Distance(target.position, gameObject.transform.position);
                    if (Distance < TargetDistanceToCharge)
                    {
                        isCharging = true;
                        animator.SetBool("IsCharging", true);
                    }
                    else
                    {
                        isCharging = false;
                        animator.SetBool("IsCharging", false);
                    }


                    if (isCharging )
                    {
                        if (HeadHit.isSquashed == false)
                        {


                            // Calculate the direction from the enemy to the player.
                            Vector3 moveDirection = (target.position - transform.position).normalized;
                            Vector3 LookDirection = new Vector3(target.position.x, transform.position.y, target.position.z);

                            // Calculate the new position for the enemy.
                            Vector3 newPosition = transform.position + (moveSpeed * Time.deltaTime * new Vector3(moveDirection.x, 0, moveDirection.z));

                            // Move the enemy towards the player.
                            rb.MovePosition(newPosition);

                            transform.LookAt(LookDirection);
                        }
                    }
                }
            }
        }

        
        /*else if(!isCharging)
        {
            transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);
        }*/




        // Check if the player's transform (target) is not null.

    }
}

