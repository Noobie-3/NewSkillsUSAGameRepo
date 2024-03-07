using Kino;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewThirdPerson : MonoBehaviour
{
    // Movement variables
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;
    public Transform orientation;
    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;
    [HideInInspector] public bool IsSprinting;

    // Input variables
    [Header("Input")]
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;

    // Keybinds
    [Header("Keybinds")]
    public KeyCode jumpKey;
    public KeyCode sprintKey;

    // Ground check variables
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    Vector3 TempGravity;
    Vector3 DefaultGravity = new Vector3(0f, -9.8f, 0f);
    private bool IsFalling;
    public float GroundedTimer;
    public float TimeToJumpAfterGround;
    public bool JumpUsed;

    // Miscellaneous variables
    [Header("Misc. Vars")]
    Rigidbody rb;
    Animator anim;
    public bool canRewind;
    // Maximum recording duration in seconds
    public float maxRecordingDuration = 5.0f;
    public float currentRecordingTime;
    public float totalRecordedTime;
    public bool isrewinding;
    public GameController GC;
    public static NewThirdPerson Instance;

    // New variables for jump height and gravity
    public float jumpHeight = 2.0f;
    public float gravity = -9.8f;
    private bool isJumping = false;

    private void Start()
    {
        if (NewThirdPerson.Instance == null)
        {
            NewThirdPerson.Instance = this;
            //make this the player for everything to have easy access
        }
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        anim = GetComponentInChildren<Animator>();
        TempGravity = DefaultGravity;
    }

    private void Update()
    {
        if (GameController.instance.IsPaused != true && canRewind)
        {
            TimeTracker();
        }
    }

    private void FixedUpdate()
    {
        if (GameController.instance.IsPaused == false)
        {
            MovePlayer();
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f, whatIsGround);
            MyInput();
            SpeedControl();
            GroundPhysics();
            AirPhysics();
        }
        else
        {
            anim.speed = 0;

            if (Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene("MainScene");
                //change this later using a separate Script Please
            }
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Animate();

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && !JumpUsed)
        {
            
                Jump(jumpForce);
                isJumping = true;
                JumpUsed = true;
            

            IsSprinting = Input.GetKey(sprintKey);
        }
    }

    public void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);

        // in air
        else if (!grounded)
            transform.Translate(moveDirection.normalized * moveSpeed * airMultiplier * Time.deltaTime);
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
        }
    }

    // Modified Jump method without using Rigidbody
    public void Jump(float jumpHeight)
    {
        float jumpVelocity = Mathf.Sqrt(2 * jumpHeight * -gravity);
        transform.position += Vector3.up * jumpVelocity;
        Debug.Log("Jumping!");
    }

    private void ResetJump()
    {
        readyToJump = true;
        anim.SetBool("Jump", false);
        JumpUsed = false;
    }

    private void Animate()
    {
        if (anim.speed == 0)
        {
            anim.speed = 1;
        }
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

    public void GroundPhysics()
    {
        // handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
            GroundedTimer = 0;
            ResetJump();
        }
        else
            rb.drag = 0;

        // Ledge Forgiveness
        if (!grounded && GroundedTimer < TimeToJumpAfterGround)
        {
            GroundedTimer += Time.deltaTime;
            readyToJump = true;
        }
        else if (!grounded && GroundedTimer > TimeToJumpAfterGround)
        {
            readyToJump = false;
        }
    }

    public void AirPhysics()
    {
        // accelerate faster when falling
        if (rb.velocity.y < 0 && !grounded)
        {
            IsFalling = true;
        }
        else if (grounded)
        {
            IsFalling = false;
        }

        // Falling Animation
        if (IsFalling)
        {
            anim.SetBool("IsFalling", true);
        }
        else if (!IsFalling && anim.GetBool("IsFalling") == true)
        {
            anim.SetBool("IsFalling", false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Moving_Obj")
        {
            // This will make the player a child of the Obstacle
            gameObject.transform.parent = other.gameObject.transform;
            rb.velocity = other.GetComponent<Rigidbody>().velocity;
            Debug.Log("The player is now a child of " + gameObject.transform.parent.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        transform.parent = null;
    }
}
