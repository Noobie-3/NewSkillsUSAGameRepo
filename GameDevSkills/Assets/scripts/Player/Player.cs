using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 camRotation;
    private Transform cam;
    [Range(-45, -15)]
    public int minAngle = -30;
    [Range(30, 80)]
    public int maxAngle = 45;
    [Range(50, 500)]
    public int sensitivity = 200;
    Animation A;
    GameController GC;
    MainMusicManager TEST;
    public Rigidbody rb;
    public Animator animator;
    public TimeRewinderV2 TR;
    public bool isGrounded = false;
    public bool canDoubleJump = false;
    public bool Punch1;



    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        A = gameObject.GetComponent<Animation>();
        animator = gameObject.GetComponent<Animator>();
        GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
        TEST = GameObject.FindWithTag("GC").GetComponent<MainMusicManager>();
    }

    public  void Update() {
        if (!gameObject.GetComponent<TimeRewinderV2>().Isrewinding)
        {
            /*        base.TimeUpdate();
            */
            Vector3 pos = transform.position;


            if (Input.GetKey(KeyCode.W))//Idle to walk and move forward
            {

                transform.position += Camera.main.transform.forward * GC.speed * Time.deltaTime;
                animator.SetTrigger("IsWalking");
            }

            //reset walk to idle
            else if (!Input.GetKeyDown(KeyCode.W) || (!Input.GetKeyDown(KeyCode.S)))
            {
                animator.ResetTrigger("IsWalking");
                animator.ResetTrigger("IsRunning");

            }



            //checks if going Left so that you can use strafe animation



            if (Input.GetKeyDown(KeyCode.A)) {

                animator.SetTrigger("GoingLeft");

            }
            else if(Input.GetKeyUp(KeyCode.A))
            {
                animator.ResetTrigger("GoingLeft");

            }   
                
                //checks if going right so that you can use strafe animation
                 
            if(Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.W)) {

                animator.SetTrigger("GoingRight");

            }

            else if(Input.GetKeyUp(KeyCode.D))
            {
                animator.ResetTrigger("GoingRight");

            }
            



            //move Backwards
            if (Input.GetKey(KeyCode.S))
            {
                // pos.z -= MoveSpeed * Time.deltaTime;
                transform.position -= Camera.main.transform.forward * GC.speed * Time.deltaTime;
            }

            //move right
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += Camera.main.transform.right * GC.speed * Time.deltaTime;
            }
             
            //move left
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= Camera.main.transform.right * GC.speed * Time.deltaTime;
            }


            //Toggle Run
            if (Input.GetKey(KeyCode.LeftShift))
            {
                GC.speed = GC.DefaultMoveSpeed * 1.5f;
                animator.SetTrigger("IsRunning");
                TEST.PlaySoundEffect("BattleMusic");
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                GC.speed = GC.DefaultMoveSpeed;
                animator.ResetTrigger("IsRunning");

            }

            //AttacksMAY MOVE TO ANOTHER SCRIPT
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Punch1");
            }
            if (Input.GetMouseButtonUp(0))
            {
                animator.ResetTrigger("Punch1");
            }

            //JUMP
            if (Input.GetKey(KeyCode.Space))
            {
                // Check if the player is grounded using raycasting.
                isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f);

                // Reset double jump ability when grounded.
                if (isGrounded)
                {
                    canDoubleJump = false;
                }

                // Jump
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    if (isGrounded)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, GC.JumpForce, rb.velocity.z);
                    }
                    else if (!canDoubleJump)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, GC.JumpForce, rb.velocity.z);
                        canDoubleJump = true;
                    }
                }




            }
            // transform.position = pos;



            transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * Input.GetAxis("Mouse X"));

            camRotation.x -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            camRotation.x = Mathf.Clamp(camRotation.x, minAngle, maxAngle);

        }


    }



    private void OnTriggerStay(Collider other)
    {
        if (other.name == "HitBox" && other.tag == "HitBox" && GC.TimeTillDamageAgain <= 0)
        {
            SimpleEnenmy enemy = other.GetComponentInParent<SimpleEnenmy>();

            // Take damage from enemy
            GC.PlayerHP = GC.TakeDamage(enemy.damage, GC.PlayerHP, gameObject);
            GC.TimeTillDamageAgain = 3;

            print(GC.PlayerHP);

        }
    }




}
