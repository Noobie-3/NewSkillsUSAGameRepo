using Unity.VisualScripting;
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

    [Header("Input")]
    private float horizontalInput;
    private float verticalInput;

    [Header("Physics")]
    public float gravity;
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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        ApplyGravity();
        GroundCheck();
        MyInput();
        MovePlayer();
        Animate();
    }

    void MovePlayer()
    {
        Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
    }

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
    }

    public void Jump(float jumpHeight)
    {        
        //set velocity to 0
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        //add jump velocity
         jumpHeight = jumpForce * jumpCurve.Evaluate(0); // Evaluate the jump curve at the beginning
        rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
        isJumping = true;
        Invoke("ResetIsJumping", jumpCooldown);
    }

    private void ApplyGravity()
    {
        if (!grounded && !isJumping) // Apply gravity only if not grounded and not jumping
            rb.velocity += gravity * Time.fixedDeltaTime * Vector3.up;
    }

    private void Animate()
    {
        anim.SetFloat("horizontal", horizontalInput);
        anim.SetFloat("vertical", verticalInput);
        anim.SetBool("Jump", isJumping);
        anim.SetBool("IsFalling", IsFalling);
    }

    private void GroundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f, whatIsGround);
        if (!grounded && rb.velocity.y <= 0)
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
    }

    private void ResetJump()
    {
        JumpUsed = false;
        isJumping = false;
        readyToJump = true;
    }
    private void ResetIsJumping()
    {
        isJumping = false;
    }
}
