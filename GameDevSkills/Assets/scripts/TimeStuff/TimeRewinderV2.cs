using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewinderV2 : MonoBehaviour
{
    public bool Isrewinding = false;
    private List<PointInTime> PointsInTime;
    private Rigidbody rb;
    public Animator animator;
    public float totalRecordedTime;
    private CharacterController m_Controller;


    // Flags to control what to record
    public bool Record_Position;
    public bool Record_Rotation;
    public bool Record_Velocity;
    public bool Record_BlendState; // New flag to record blend state

    // Maximum recording duration in seconds
    public float maxRecordingDuration = 5.0f;
    public float currentRecordingTime;

    // Blend parameter name
    public string blendParameterH = "horizontal"; // Name of the blend parameter
    public string blendParameterV = "vertical"; // Name of the blend parameter

    float blendValueV;

    private void Start()
    {
        PointsInTime = new List<PointInTime>();
        if (GetComponent<Animator>()){
            animator = gameObject.GetComponent<Animator>();
        
        }

        if(GetComponent<CharacterController>())
        {
            m_Controller = GetComponent<CharacterController>();

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentRecordingTime >= maxRecordingDuration)
        {
            StartRewind();
/*            if (gameObject.GetComponent<Rigidbody>())
            {
                rb.isKinematic = true;
            }*/
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            StopRewind();
/*            if (gameObject.GetComponent<Rigidbody>())
            {
                rb.isKinematic = false;
            }*/
        }

        if (currentRecordingTime < maxRecordingDuration && !Isrewinding)
        {
            currentRecordingTime += Time.deltaTime;
        }
        else if (currentRecordingTime >= maxRecordingDuration)
        {
            currentRecordingTime = maxRecordingDuration;
        }

        // Check if total recorded time exceeds the maximum duration
        totalRecordedTime = GetTotalRecordedTime();
        if (totalRecordedTime > maxRecordingDuration)
        {
            // Remove the oldest recorded data point
            PointsInTime.RemoveAt(PointsInTime.Count - 1);
        }
    }

    private void FixedUpdate()
    {
        if (Isrewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    public void Record()
    {

        if(Record_BlendState && Record_Velocity)
        {
            float blendValueH = animator.GetFloat(blendParameterH); // Get the blend value
            float blendValueV = animator.GetFloat(blendParameterV); // Get the blend value

            PointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, m_Controller.velocity, blendValueH, blendValueV, Record_Position, Record_Rotation, Record_Velocity, Record_BlendState));
            print("StateAdded");

        }

        else if(!Record_BlendState && !Record_Velocity )
        {
            PointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, Record_Position, Record_Rotation));
        }
        else if(!Record_Velocity && Record_BlendState)
        {
            float blendValueH = animator.GetFloat(blendParameterH); // Get the blend value
            float blendValueV = animator.GetFloat(blendParameterV); // Get the blend value

            PointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, blendValueH, blendValueV, Record_Position, Record_Rotation, Record_BlendState));

        }
        else if(Record_Velocity && !Record_BlendState)
        {
            PointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, m_Controller.velocity , Record_Position, Record_Rotation, Record_Velocity));

        }
    }

    private void Rewind()
    {
        currentRecordingTime -= Time.deltaTime;
        if (PointsInTime.Count > 0)
        {
            if (Record_Position)
            {
                transform.position = PointsInTime[0].position;
            }
            if (Record_Rotation)
            {
                transform.rotation = PointsInTime[0].rotation;
            }
            if (Record_Velocity)
            {
                m_Controller.velocity.Set(PointsInTime[0].Velocity.x, PointsInTime[0].Velocity.y,PointsInTime[0].Velocity.z);
            }
            if (Record_BlendState)
            {
                 animator.SetFloat( blendParameterV, PointsInTime[0].blendValueV);
                print(PointsInTime[0].blendValueV);
                 animator.SetFloat( blendParameterH, PointsInTime[0].blendValueH);
            }
            PointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    public void StartRewind()
    {
        Isrewinding = true;
    }

    public void StopRewind()
    {
        Isrewinding = false;
    }

    // Calculate the total recorded time
    private float GetTotalRecordedTime()
    {
        float totalRecordedTime = 0.0f;
        foreach (PointInTime point in PointsInTime)
        {
            totalRecordedTime += Time.fixedDeltaTime;
        }
        return totalRecordedTime;
    }
}
