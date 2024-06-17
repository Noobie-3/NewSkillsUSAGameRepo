using Unity.VisualScripting; // Unused namespace
using UnityEngine;

public class REVAMPEDPLAYERCONTROLLER : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Transform orientation;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public AnimationCurve jumpCurve;
    public float TimeAfterJump;
    public float TimetoJump;

    [Header("Input")]
    private float horizontalInput;
    private float verticalInput;

    [Header("Physics")]
    public float gravity;
    public float Tempgravity;
    public float airMultiplier;
    public float playerHeight;
    public bool IsFalling;
    public LayerMask whatIsGround;

    [Header("Keybinds")]
    public KeyCode jumpKey;
    public KeyCode sprintKey;

    [Header("Misc")]
    public Animator anim;
    public Rigidbody rb;
    public bool isJumping = false;
    public bool JumpUsed = false;
    public bool readyToJump = true;
    public bool grounded;
    public bool isRewinding;
    public float maxRecordingDuration;
    public bool canRewind;
    public float currentRecordingTime;
    public NewSceneInfo NewSceneInfo;
    public AudioSource JumpSound;

    void Start()
    {
        DontDestroyOnLoad(this);
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        Tempgravity = gravity;
    }

    void FixedUpdate()
    {
        if (GameController.instance != null)
        {
            if (GameController.instance.IsPaused != true)
            {

                TimeTracker(); // Track time for recording and rewinding
                ApplyGravity(); // Apply gravity if not grounded or jumping
                GroundCheck(); // Check if the player is grounded
                MyInput(); // Get input from player
                Animate(); // Update animator parameters
            }

            PauseEffects();
        }
        
    }

    // Move the player based on input
    public void MovePlayer()
    {
        Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    // Get input from player
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey) && readyToJump && !JumpUsed)
        {
            Jump(jumpForce);
            isJumping = true;
            JumpUsed = true;
            readyToJump = false;
        }

        if (horizontalInput != 0 || verticalInput != 0)
        {
            MovePlayer();
        }
    }

    // Perform a jump
    public void Jump(float jumpHeight)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.isKinematic = false;
        jumpHeight = jumpForce * jumpCurve.Evaluate(0); // Evaluate the jump curve at the beginning
        rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
        isJumping = true;

        if (JumpSound != null)
        {
            JumpSound.Play();
        }

    }

    // Apply gravity to the player
    private void ApplyGravity()
    {
        
            rb.velocity += gravity * Time.fixedDeltaTime * Vector3.up;
    }

    // Update animator parameters
    private void Animate()
    {
        anim.SetFloat("horizontal", horizontalInput);
        anim.SetFloat("vertical", verticalInput);
        anim.SetBool("Jump", isJumping);
        anim.SetBool("IsFalling", IsFalling);
    }

    // Check if the player is grounded
    private void GroundCheck()
    {
        //add raycast hit to this to see what layer its hitting
        grounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit HitInfo, playerHeight,whatIsGround);

         
        //show raycast
        Debug.DrawRay(transform.position, Vector3.down * playerHeight, Color.yellow);
        if (!grounded && rb.velocity.y <= 0 && TimeAfterJump >= TimetoJump)
        {
            IsFalling = true;
        }
        else
        {
            IsFalling = false;
        }

        if (grounded)
        {
            ResetJump();
        }
        else if (!grounded && TimetoJump > TimeAfterJump)
        {
            TimeAfterJump += Time.deltaTime;
        }
    }

    // Reset jump-related variables
    private void ResetJump()
    {
        JumpUsed = false;
        isJumping = false;
        readyToJump = true;
        TimeAfterJump = 0;
    }


    // Track time for recording and rewinding
    public void TimeTracker()
    {
        if (currentRecordingTime < maxRecordingDuration && !isRewinding)
        {
            currentRecordingTime += Time.deltaTime;
        }
        else if (currentRecordingTime >= maxRecordingDuration)
        {
            currentRecordingTime = maxRecordingDuration;
        }
        if (isRewinding)
        {
            currentRecordingTime -= Time.deltaTime;
        }
        if (currentRecordingTime <= 0)
        {
            isRewinding = false;
        }
    }

    private void PauseEffects()
    {



        if (GameController.instance.IsPaused)
        {
            if (anim.speed == 1)
            {
                anim.speed = 0;
            }

            if(gravity != 0)
            {
                gravity = 0;
            }
        }
        else
        {
            if (anim.speed == 0)
            {
                anim.speed = 1;
            }

            if(gravity == 0)
            {
                gravity = Tempgravity;
            }
        }

        
    }



    // Apply knockback to the player
    public void ApplyKnockback(Vector3 direction)
    {
        // Add force in the specified direction
        rb.velocity = direction;
    }
}
