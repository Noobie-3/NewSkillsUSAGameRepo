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

    // Flags to control what to record
    public bool Record_Position = true;
    public bool Record_Rotation = true;
    public bool Record_Velocity = true;
    public bool Record_Animation = true;

    // Maximum recording duration in seconds
    public float maxRecordingDuration = 5.0f;
    public float currentRecoringTime;

    // Flags to check if a Rigidbody and an Animator are present
    public bool Check_Rigidbody = true;
    public bool Check_Animator = true;

    private void Start()
    {
        PointsInTime = new List<PointInTime>();

        if (Check_Rigidbody)
        {
            rb = gameObject.GetComponent<Rigidbody>();
        }

        if (Check_Animator)
        {
            animator = gameObject.GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentRecoringTime >= maxRecordingDuration)
        {
            if (Check_Rigidbody && rb != null && Isrewinding)
            {
                rb.isKinematic = true;
            }
            StartRewind();
        }
        if (!Input.GetKey(KeyCode.R))
        {
            StopRewind();
        }

        if (currentRecoringTime < maxRecordingDuration && !Isrewinding)
        {
            currentRecoringTime += Time.deltaTime;
            if (Check_Rigidbody && rb != null && rb.isKinematic)
            {
                rb.isKinematic = false;
            }
        }
        else if (currentRecoringTime >= maxRecordingDuration)
        {
            currentRecoringTime = maxRecordingDuration;
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
        if ((Record_Position || Record_Rotation || Record_Velocity) && Check_Rigidbody && rb != null)
        {
            PointsInTime.Insert(0, new PointInTime(
                Record_Position ? transform.position : PointsInTime[0].position,
                Record_Rotation ? transform.rotation : PointsInTime[0].rotation,
                Record_Velocity ? rb.velocity : PointsInTime[0].Velocity,
                Check_Animator ? animator.GetCurrentAnimatorStateInfo(0) : PointsInTime[0].AnimStates));
        }
        else if (Record_Animation && Check_Animator)
        {
            PointsInTime.Insert(0, new PointInTime(
                PointsInTime[0].position,
                PointsInTime[0].rotation,
                PointsInTime[0].Velocity,
                animator.GetCurrentAnimatorStateInfo(0)));
        }
    }

    private void Rewind()
    {
        currentRecoringTime -= Time.deltaTime;
        if (PointsInTime.Count > 0)
        {
            PointInTime pointInTime = PointsInTime[0];
            PointsInTime.RemoveAt(0);

            if (Record_Position)
            {
                transform.position = pointInTime.position;
            }

            if (Record_Rotation)
            {
                transform.rotation = pointInTime.rotation;
            }

            if (Check_Rigidbody && rb != null && Record_Velocity)
            {
                rb.velocity = pointInTime.Velocity;
            }

            if (Check_Animator && Record_Animation)
            {
                animator.Play(pointInTime.AnimStates.fullPathHash, 0, pointInTime.AnimStates.normalizedTime);
            }
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
