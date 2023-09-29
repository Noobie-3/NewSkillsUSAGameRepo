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

    // Maximum recording duration in seconds
    public  float maxRecordingDuration = 5.0f;
    public float currentRecoringTime;
    private void Start()
    {
        PointsInTime = new List<PointInTime>();
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentRecoringTime >= maxRecordingDuration)
        {
            StartRewind();
            if (gameObject.GetComponent<Rigidbody>())
            {
                rb.isKinematic = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            StopRewind();
            rb.isKinematic = false;
        }

        if(currentRecoringTime < maxRecordingDuration && !Isrewinding)
        {
            currentRecoringTime += Time.deltaTime;

        }
        else if(currentRecoringTime >= maxRecordingDuration )
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
        PointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, rb.velocity, animator.GetCurrentAnimatorStateInfo(0)));
    }

    private void Rewind()
    {
        currentRecoringTime -= Time.deltaTime;
        if (PointsInTime.Count > 0)
        {
            transform.position = PointsInTime[0].position;
            transform.rotation = PointsInTime[0].rotation;
            rb.velocity = PointsInTime[0].Velocity;
            animator.Play(PointsInTime[0].AnimStates.fullPathHash, 0, PointsInTime[0].AnimStates.normalizedTime);
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
