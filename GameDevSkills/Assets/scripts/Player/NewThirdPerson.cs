
using Kino;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewThirdPerson : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;
    [HideInInspector] public bool IsSprinting;

    [Header("Keybinds")]
    public KeyCode jumpKey;
    public KeyCode sprintKey;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    Vector3 TempGravity;
    private bool IsFalling;
    public float GroundedTimer;
    public float TimeToJumpAfterGround;
    public bool JumpUsed;

    [Header("Misc. Vars")]
    Rigidbody rb;
    Animator anim;
    public bool canRewind;
    // Maximum recording duration in seconds
    public float maxRecordingDuration = 5.0f;
    public float currentRecordingTime;
    public float totalRecordedTime;
    public bool isrewinding;






    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        anim = GetComponentInChildren<Animator>();
        TempGravity = Physics.gravity;

    }

    private void Update()
    {
        TimeTracker();
        // ground check

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f, whatIsGround);
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, playerHeight * .5f, whatIsGround))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * playerHeight * .5f, Color.white);
        }

        MyInput();
        SpeedControl();

        // handle drag

        if (grounded)
        {
            rb.drag = groundDrag;
            GroundedTimer = 0;
            ResetJump();
        }
        else
            rb.drag = 0;

        //Ledge Forgivness

        if (!grounded && GroundedTimer < TimeToJumpAfterGround)
        {
            GroundedTimer += Time.deltaTime;
            readyToJump = true;
        }
        else if (!grounded && GroundedTimer > TimeToJumpAfterGround)
        {
            readyToJump = false;
        }

        // accecelerate faster when falling

        if (rb.velocity.y < 0 && !grounded)
        {
            IsFalling = true;
        }

        else if (grounded)
        {
            IsFalling = false;
        }

        //Falling Animation

        if (IsFalling)
        {
            anim.SetBool("IsFalling", true);


        }

        else if (!IsFalling && anim.GetBool("IsFalling") == true)
        {
            anim.SetBool("IsFalling", false);
        }




    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Animate();

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && !JumpUsed)
        {
            if (GroundedTimer <= TimeToJumpAfterGround | grounded)
            {
                readyToJump = false;
                Jump(jumpForce);
                print(rb.velocity + "AfterJump");
            }

            IsSprinting = Input.GetKey(sprintKey);
        }





    }

    public void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        if (rb.velocity.y < 0)
        {

            Physics.gravity = TempGravity * 1.9f;
            /*            print(Physics.gravity.y);
            */
        }


    }

    public void Jump(float JForce)
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        print(rb.velocity);

        rb.AddForce(transform.up * JForce, ForceMode.Impulse);
        anim.SetBool("Jump", true);
        JumpUsed = true;
    }
    private void ResetJump()
    {
        readyToJump = true;
        anim.SetBool("Jump", false);
        JumpUsed = false;
    }

    private void Animate()
    {
        anim.SetFloat("horizontal", horizontalInput);
        anim.SetFloat("vertical", verticalInput);
    }

    public void TimeTracker()
    {
        if (currentRecordingTime < maxRecordingDuration && !isrewinding)
        {
            currentRecordingTime += Time.deltaTime;
        }
        else if (currentRecordingTime >= maxRecordingDuration)
        {
            currentRecordingTime = maxRecordingDuration;
        }
        if (isrewinding)
        {
            currentRecordingTime -= Time.deltaTime;
        }
        if (currentRecordingTime <= 0)
        {
            isrewinding = false;
        }

    }
    void OnTriggerStay(Collider other)
    { 
        if (other.gameObject.tag == "Moving_Obj")
        {

            //This will make the player a child of the Obstacle
            gameObject.transform.SetParent(other.gameObject.transform.root);
            if(rb.velocity.x < other.GetComponent<Rigidbody>().velocity.x || rb.velocity.z < other.GetComponent<Rigidbody>().velocity.z  )
            {
            rb.velocity = other.GetComponent<Rigidbody>().velocity;

            }
        }
    
    }
    void OnTriggerExit(Collider other)
    {
        transform.parent = null;
    }
}