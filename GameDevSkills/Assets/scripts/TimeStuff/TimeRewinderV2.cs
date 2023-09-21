using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class TimeRewinderV2 : MonoBehaviour
{
    public bool Isrewinding =  false;
    List<PointInTime> PointsInTime;
    Rigidbody rb;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        PointsInTime = new List<PointInTime>();
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            StartRewind();
            if(gameObject.GetComponent<Rigidbody>())
            {
                rb.isKinematic = true;

            }
            
        }
        if (Input.GetKeyUp(KeyCode.R)) {
            StopRewind();
            rb.isKinematic = false;
        }
    }
    private void FixedUpdate() {

        if(Isrewinding) {
            Rewind();
        }
        else {
            Record();
        }
    }
    public void Record() {
        PointsInTime.Insert(0, new PointInTime( transform.position, transform.rotation, rb.velocity, animator.GetCurrentAnimatorStateInfo(0)));
    }

    private void Rewind()
    {
        if (PointsInTime.Count > 0)
        {
            transform.position = PointsInTime[0].position;
            transform.rotation = PointsInTime[0].rotation;
            rb.velocity = PointsInTime[0].Velocity;
            animator.Play(PointsInTime[0].AnimStates.fullPathHash, 0 , PointsInTime[0].AnimStates.normalizedTime);
            PointsInTime.RemoveAt(0);
        }
        else StopRewind();
    }
    public void StartRewind()
    {
        Isrewinding = true;
    }
    public void StopRewind()
    {
        Isrewinding = false;
    }
}
