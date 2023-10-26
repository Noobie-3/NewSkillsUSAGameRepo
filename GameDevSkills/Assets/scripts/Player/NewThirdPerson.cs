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

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey ;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    Animator anim;

    public float Gravity;

    Vector3 TempGravity;
    private bool IsFalling;
    public float GroundedTimer;
    public float TimeToJumpAfterGround;
    public bool JumpUsed;

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
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .05f, whatIsGround);

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

        if(!grounded && GroundedTimer < TimeToJumpAfterGround)
        {
            GroundedTimer += Time.deltaTime;
            readyToJump = true;
        }
        else if( !grounded && GroundedTimer > TimeToJumpAfterGround)
        {
            readyToJump = false;
        }
        
        if (rb.velocity.y < 0 && !grounded)//Falling Animation
        {
            IsFalling = true;
        }

        else if (grounded)
        {
            IsFalling = false;
        }

        if (IsFalling)
        {
            anim.SetBool("IsFalling", true);

            
        }
        else if(!IsFalling && anim.GetBool("IsFalling") == true)
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

                
/*                Invoke(nameof(ResetJump), jumpCooldown);
*/            }
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

        if(rb.velocity.y < 0)
        {

            Physics.gravity = TempGravity * 1.9f;
/*            print(Physics.gravity.y);
*/        }


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
}
