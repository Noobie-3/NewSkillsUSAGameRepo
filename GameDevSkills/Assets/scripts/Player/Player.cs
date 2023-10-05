using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 camRotation;

    private Transform cam;

    [Range(-45, -15)]

    [SerializeField]
    int minAngle = -30;
    [Range(30, 80)]

    [SerializeField]
    public int maxAngle = 45;

    [SerializeField]
    [Range(50, 500)]
    public int sensitivity = 200;

    Animation A;

    GameController GC;

    MainMusicManager TEST;

    [SerializeField]
    public Rigidbody rb;

    [SerializeField]
    Animator animator;

    [SerializeField]
    TimeRewinderV2 TR;

    [SerializeField]
    bool isGrounded = false;

    [SerializeField]
    bool isJumping;

    [SerializeField]
    bool canDoubleJump = false;

    [SerializeField]
    bool Punch1;

    [SerializeField]
    int MaxJumps;

    [SerializeField]
    int JumpsLeft;

    [SerializeField]
    decimal maxValue;







    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        A = gameObject.GetComponent<Animation>();
        animator = gameObject.GetComponent<Animator>();
        GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
        TEST = GameObject.FindWithTag("GC").GetComponent<MainMusicManager>();
    }

    void Update()
    {

        if (!gameObject.GetComponent<TimeRewinderV2>().Isrewinding)
        {
            Move();

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




            // transform.position = pos;



            transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * Input.GetAxis("Mouse X"));

            camRotation.x -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            camRotation.x = Mathf.Clamp(camRotation.x, minAngle, maxAngle);


        }



    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            JumpsLeft = MaxJumps;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
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

    private void Move() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction based on input
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);

        // Normalize the direction vector to ensure consistent speed in all directions
        moveDirection.Normalize();
        Vector3 moveVelocity = moveDirection * GC.speed;
            transform.Translate(moveVelocity * Time.deltaTime);
        animator.SetFloat("vertical", .4f);

        //Toggle Run
        if (Input.GetKey(KeyCode.LeftShift))
            {
                GC.speed = GC.DefaultMoveSpeed * 1.5f;
                maxValue = 1;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                GC.speed = GC.DefaultMoveSpeed;
            maxValue = .4m;

            }
        decimal DecHorizValue = (decimal)horizontalInput;
        decimal HorizPercentage = (DecHorizValue / maxValue);
        print(HorizPercentage);
        

       

        //Jump
        isGrounded = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), .1f);

        if (isGrounded)
        {
            JumpsLeft = MaxJumps;
        }
        if (Input.GetKeyDown(KeyCode.Space) && JumpsLeft > 0)
        {
            // Check if the player is grounded using raycasting.

            rb.AddForce(Vector3.up * GC.JumpForce, ForceMode.Impulse);

            JumpsLeft--;


        }

    }






}
