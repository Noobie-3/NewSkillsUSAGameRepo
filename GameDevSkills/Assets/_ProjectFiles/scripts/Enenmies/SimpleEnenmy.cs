using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnenmy : MonoBehaviour 
{
    public float moveSpeed = 3.0f; // Adjust this to control the speed of the enemy.
    public Transform target; // The player's transform.
    public float damage = 2;

    public float min = 2f;
    public float max = 3f;
    public bool isCharging;
    public float HP = 1;
    public NewThirdPerson NTP;
    public Rigidbody rb;
    public Animator animator;

    HeadDetection HeadHit;
    // Use this for initialization


    // Update is called once per frame


    private void Awake() {
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

        min = transform.position.x;
        max = transform.position.x + 3;
        if (gameObject.GetComponentInChildren<HeadDetection>() is not null)
        {
            HeadHit = gameObject.GetComponentInChildren<HeadDetection>();
        }
        target = GameController.instance.Player.transform;

    }

    private void OnTriggerStay(Collider other)
    {if (GameController.instance.isDead == false)
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

        if (GameController.instance.IsPaused == false)

        {
            if (GameController.instance.IsPaused == false && animator.speed != 1)
            {
                animator.speed = 1;
            }

            if (!GameController.instance.Player.GetComponent<TimeRewinderV2>().Isrewinding)
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
                        rb.MovePosition(newPosition);

                        transform.LookAt(LookDirection);
                    }
                }
            }
        }
        else if (GameController.instance.IsPaused)
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

