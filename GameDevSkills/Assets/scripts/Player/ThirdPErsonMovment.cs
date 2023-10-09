using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPErsonMovment : MonoBehaviour
{

    [Header("Movement")]
    public float MoveSpeed;
    public float groundDrag;


    public Transform Direction;

    float HorizontalInput;
    float VerticalInput;

    Vector3 MoveDir;

    Rigidbody rb;

    [Header("Ground Check")]

    public float PlayerHeight;
    public LayerMask WhatIsGround;
    public bool IsGrounded;
    public float JumpForce;
    public float jumpCooldown;
    public float AirMult;
    public bool ReadyToJump;
    public KeyCode JumpKey = KeyCode.Space;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, WhatIsGround);
        if (IsGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        INputHandeler();
        SpeedControl();


    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    private void INputHandeler() {


        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(JumpKey) && ReadyToJump && IsGrounded)
        {
            ReadyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void movePlayer(){


        MoveDir = Direction.forward * VerticalInput + Direction.right * HorizontalInput;
        if(IsGrounded)
        {
            rb.AddForce(MoveDir.normalized * MoveSpeed * 10, ForceMode.Force);

        }
        else if(!IsGrounded)
        {
            rb.AddForce(MoveDir.normalized * MoveSpeed * 10f * AirMult, ForceMode.Force);

        }

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > MoveSpeed)
        {
            Vector3 LimitedVel = flatVel.normalized * MoveSpeed;
            rb.velocity = new Vector3(LimitedVel.x, rb.velocity.y, LimitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        ReadyToJump = false;

    }

    private void ResetJump() {
        ReadyToJump = true;
    }
}
