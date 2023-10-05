using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class JumpHandeler : MonoBehaviour
{

    public PlayerInput Input { get; protected set; }

    private CharacterController _characterController;
    private CharacterController m_Controller;
    [SerializeField]
    bool isGrounded = false;
    [SerializeField]
    int MaxJumps;
    GameController GC;

    [SerializeField]
    int JumpsLeft;

    // Start is called before the first frame update
    void Start()
    {
        Input = GetComponent<PlayerInput>();
        Input.Init();
        m_Controller = GetComponent<CharacterController>();
        GC = GameObject.FindWithTag("GC").GetComponent<GameController>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.Jump)
        {
            Jump();
        }
    }
    void Jump()
    {
        isGrounded = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), .1f);

        if (isGrounded)
        {
            JumpsLeft = MaxJumps;
        }

        _characterController.SimpleMove(Vector3.up * GC.JumpForce);

        JumpsLeft--;


        
    }
}
