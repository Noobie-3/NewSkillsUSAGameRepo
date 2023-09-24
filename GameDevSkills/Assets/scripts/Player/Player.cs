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


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        A = gameObject.GetComponent<Animation>();
        animator = gameObject.GetComponent<Animator>();
        GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
        TEST = GameObject.FindWithTag("GC").GetComponent<MainMusicManager>();
    }

    public  void FixedUpdate() {

/*        base.TimeUpdate();
*/        Vector3 pos = transform.position;


            if (Input.GetKey(KeyCode.W))//Idle to walk and move forward
            {

                transform.position += Camera.main.transform.forward * GC.speed * Time.deltaTime;
                animator.SetTrigger("IsWalking");
            }
            else if (!Input.GetKeyDown(KeyCode.W) ||(!Input.GetKeyDown(KeyCode.S)))
            {
                animator.ResetTrigger("IsWalking");
                animator.ResetTrigger("IsRunning");

            }


            if (Input.GetKey(KeyCode.S))
            {
                // pos.z -= MoveSpeed * Time.deltaTime;
                transform.position -= Camera.main.transform.forward * GC.speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.position += Camera.main.transform.right * GC.speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= Camera.main.transform.right * GC.speed * Time.deltaTime;
            }

            if(Input.GetKey(KeyCode.LeftShift))
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

            if (Input.GetKey(KeyCode.Space))
            {
            // Check if the player is grounded using raycasting.
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.5f);

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



    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Hitbox" && other.gameObject.tag == "Enemy")
        {
            SimpleEnenmy enemy = other.GetComponentInParent<SimpleEnenmy>();

            // Take damage from enemy
            if (/*enemy.canAttack() &&*/ !GC.IsInvincable)
            {
                GC.takeDamage(enemy.damage);
                GC.TimeTillDamageAgain = 3;
                print("Test");
            }
        }
    }




}
