using Cinemachine;
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
    public bool Record_Position;
    public bool Record_Rotation;
    public bool Record_Velocity;
    public bool Record_BlendState; // New flag to record blend state
    public bool Record_Cam; // record cam postion and rotation
    public bool Record_Points;
    public CinemachineFreeLook Freecam;

    // Maximum recording duration in seconds
    public float maxRecordingDuration = 5.0f;
    public float currentRecordingTime;

    // Blend parameter name
    public string blendParameterH = "horizontal"; // Name of the blend parameter
    public string blendParameterV = "vertical"; // Name of the blend parameter

    float blendValueV;
    public bool CanRewind;
    public MoveAlongwayPoints PointsToTrack;
    private void Start()
    {
        PointsInTime = new List<PointInTime>();

        if (GetComponent<Animator>())
        {
            animator = gameObject.GetComponent<Animator>();

        }

        if (GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();

        }

        if (GetComponent<MoveAlongwayPoints>() != null)
        {
            PointsToTrack = gameObject.GetComponent<MoveAlongwayPoints>();

        }
        Freecam = GameObject.Find("CM FreeLook1").GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {

        if (GetComponent<MoveAlongwayPoints>() != null && PointsToTrack != null)
        {
            PointsToTrack = gameObject.GetComponent<MoveAlongwayPoints>();

        }

        if (GameController.instance.Player != null)
        {
            CanRewind = GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().canRewind;
            Isrewinding = GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().isRewinding;

        }

        if (CanRewind)
        {
            if (Input.GetKeyDown(KeyCode.R) && GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().currentRecordingTime >= GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().maxRecordingDuration)
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


            // Update the total recorded time
            totalRecordedTime = GetTotalRecordedTime();
            // If the total recorded time exceeds the maximum recording duration            
            // remove the oldest recorded data point

            if (totalRecordedTime > maxRecordingDuration)
            {
                // Remove the oldest recorded data point
                PointsInTime.RemoveAt(PointsInTime.Count - 1);
            }
        }

    }

    private void FixedUpdate()
    {
        if (CanRewind)
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
    }

    public void Record()
    {


        if (Record_BlendState && Record_Velocity)
        {
            float blendValueH = animator.GetFloat(blendParameterH); // Get the blend value
            float blendValueV = animator.GetFloat(blendParameterV); // Get the blend value
            float LookDirX = Freecam.m_XAxis.Value;
            float LookDirY = Freecam.m_YAxis.Value;
            if (PointsToTrack != null)
            {
                int Point = PointsToTrack.currentWayPoint;
                Vector3 Current_T = PointsToTrack.Current_Target;
                Vector3 Last_Pos = PointsToTrack.LastPos;

                PointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, rb.velocity, blendValueH, blendValueV, LookDirX, LookDirY, Record_Position, Record_Rotation, Record_Velocity, Record_BlendState, Record_Cam, Point, Current_T, Last_Pos));

            }
            else
            {
                PointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, rb.velocity, blendValueH, blendValueV, LookDirX, LookDirY, Record_Position, Record_Rotation, Record_Velocity, Record_BlendState, Record_Cam));
                print("StateAdded");
            }
        }

        else if (!Record_BlendState && !Record_Velocity)
        {
            {
                if (PointsToTrack != null)
                {
                    int Point = PointsToTrack.currentWayPoint;
                    Vector3 Current_T = PointsToTrack.Current_Target;
                    Vector3 Last_Pos = PointsToTrack.LastPos;
                    float LookDirX = Freecam.m_XAxis.Value;
                    float LookDirY = Freecam.m_YAxis.Value;
                    PointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, LookDirX, LookDirY, Record_Position, Record_Rotation, Record_Cam, Point, Current_T, Last_Pos));
                }
                else
                {

                    float LookDirX = Freecam.m_XAxis.Value;
                    float LookDirY = Freecam.m_YAxis.Value;
                    PointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, LookDirX, LookDirY, Record_Position, Record_Rotation, Record_Cam));
                }
            }
        }
        else if (!Record_Velocity && Record_BlendState)
        {
            float LookDirX = Freecam.m_XAxis.Value;
            float LookDirY = Freecam.m_YAxis.Value;
            float blendValueH = animator.GetFloat(blendParameterH); // Get the blend value
            float blendValueV = animator.GetFloat(blendParameterV); // Get the blend value

            PointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, blendValueH, blendValueV, LookDirX, LookDirY, Record_Position, Record_Rotation, Record_BlendState, Record_Cam));

        }
        else if (Record_Velocity && !Record_BlendState)
        {
            float LookDirX = Freecam.m_XAxis.Value;
            float LookDirY = Freecam.m_YAxis.Value;
            PointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, rb.velocity, Record_Position, LookDirX, LookDirY, Record_Rotation, Record_Velocity, Record_Cam));

        }
    }


    private void Rewind()
    {
        if (Record_Cam)
        {
            Freecam.m_XAxis.Value = PointsInTime[0].XLook;
            Freecam.m_YAxis.Value = PointsInTime[0].YLook;
        }

        currentRecordingTime -= Time.deltaTime;
        if (PointsInTime.Count > 0)
        {
            if (Record_Position)
            {
                transform.position = PointsInTime[0].position;
            }
            if (Record_Rotation)
                if (Record_Rotation)
                {
                    transform.rotation = PointsInTime[0].rotation;
                }
            if (Record_Velocity)
            {
                rb.velocity.Set(-PointsInTime[0].Velocity.x, -PointsInTime[0].Velocity.y, -PointsInTime[0].Velocity.z);
            }
            if (Record_BlendState)
            {
                animator.SetFloat(blendParameterV, PointsInTime[0].blendValueV);
                print(PointsInTime[0].blendValueV);
                animator.SetFloat(blendParameterH, PointsInTime[0].blendValueH);
            }
            if (PointsToTrack != null)
            {
                PointsToTrack.currentWayPoint = PointsInTime[0].Points;
                PointsToTrack.LastPos = PointsInTime[0].Last_pos;
                print(PointsInTime[0].Last_pos + "This is the value that is getting rewindded");
                PointsToTrack.Current_Target = PointsInTime[0].Current_T;
            }
            PointsInTime.RemoveAt(0);
        }

    }

    public void StartRewind()
    {
        GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().isRewinding = true;
    }

    public void StopRewind()
    {
        GameController.instance.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().isRewinding = false;
        
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
