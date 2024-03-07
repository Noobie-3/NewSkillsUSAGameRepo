using Cinemachine;
using Kino;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform combatLookAt;

    public GameObject thirdPersonCam;
    public GameController GC;
    public AnalogGlitch aG;
    public DigitalGlitch DG;
    PostProcessVolume PPV;
    public CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
    }

    private void Start()
    {
        GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
/*        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/
        aG = gameObject.GetComponent<AnalogGlitch>();
        DG = gameObject.GetComponent<DigitalGlitch>();
        PPV = GameObject.FindWithTag("Effects").GetComponent<PostProcessVolume>();
    }

    private void Update()
    {
        if (GC.IsPaused ==false)
        {
            if(GetComponent<CinemachineBrain>().enabled == false)
            {
                GetComponent<CinemachineBrain>().enabled = true;
            }
            if (gameObject.GetComponent<TimeRewinderV2>().Isrewinding)
            {
                aG.enabled = true;
                DG.enabled = true;
                PPV.enabled = true;
            }
            else
            {
                aG.enabled = false;
                DG.enabled = false;
                PPV.enabled = false;

            }

            // switch styles
            if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic);


            // rotate orientation
            Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            orientation.forward = viewDir.normalized;

            // roate player object
            if (currentStyle == CameraStyle.Basic || currentStyle == CameraStyle.Topdown)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

                if (inputDir != Vector3.zero)
                    playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }

            else if (currentStyle == CameraStyle.Combat)
            {
                Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
                orientation.forward = dirToCombatLookAt.normalized;

                playerObj.forward = dirToCombatLookAt.normalized;
            }
        }
        else
        {
            GetComponent<CinemachineBrain>().enabled = false;
        }


    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        thirdPersonCam.SetActive(false);

        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);


        currentStyle = newStyle;
    }
}
