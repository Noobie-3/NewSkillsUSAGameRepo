using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;


public class ThirdPersonCharacterController : MonoBehaviour
{
    [SerializeField] private PlayerInputActions m_Actions;
    [SerializeField] private InputAction Move;

    [SerializeField] private CharacterController m_Controller;
    [SerializeField] private Animator m_Animator;

    [SerializeField] private TimeRewinderV2 TR;

    [SerializeField] private float MovementForce;
    [SerializeField] private float JumpForce;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private Vector3 ForceDirection;
    [SerializeField] private Rigidbody rb;



    [SerializeField] Camera Cam;

    private void Awake()
    {
        /*            Init();
        */
        m_Actions = new PlayerInputActions();
        TR = GetComponent<TimeRewinderV2>();
        rb = GetComponent<Rigidbody>();

    }


    private void FixedUpdate()
    {
        ForceDirection += Move.ReadValue<Vector2>().x * getCameraRight(Cam) * MovementForce;
        ForceDirection += Move.ReadValue<Vector2>().y * getCameraForward(Cam) * MovementForce;

        rb.AddForce(ForceDirection, ForceMode.Impulse);
        ForceDirection = Vector3.zero;

        if (rb.velocity.y < 0f)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.deltaTime;
        }

        Vector3 HorizVel = rb.velocity;
        HorizVel.y = 0;
        if (HorizVel.sqrMagnitude > MaxSpeed * MaxSpeed)
        {
            rb.velocity = HorizVel.normalized * MaxSpeed + Vector3.up * rb.velocity.y;
        }

        LookAt();


    }

    private Vector3 getCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 getCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        print(right.normalized);
        return right.normalized;
    }


    private void OnEnable()
    {
        m_Actions.Player.Jump.started += DoJump;
        Move = m_Actions.Player.Move;
        m_Actions.Player.Enable();
    }



    private void OnDisable()
    {
        m_Actions.Player.Jump.started -= DoJump;
        m_Actions.Player.Disable();

    }
    private void DoJump(InputAction.CallbackContext obj)
    {
        if (grounded())
        {
            ForceDirection += Vector3.up * JumpForce;
        }
    }

    private bool grounded()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f))
        {

            return true;
        }
        else return false;
    }
    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0;

        if (Move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1)
        {
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }


    }
}

        
    





