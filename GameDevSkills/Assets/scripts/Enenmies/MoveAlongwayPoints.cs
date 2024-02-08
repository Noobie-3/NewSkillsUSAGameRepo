using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongwayPoints : MonoBehaviour
{
    public List<Vector3> WayPoints;
    public float MoveSpeed;
<<<<<<< HEAD
<<<<<<< HEAD
    public Vector3 Current_Target;
=======
    public  Vector3 Current_Target;
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
=======
    public  Vector3 Current_Target;
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
    private Vector3 FirstPos;
    public Vector3 LastPos;
    private bool IsMoving = true;
    public int currentWayPoint;
    public bool UseY;
    public bool CanMove;
    public bool canBeInterupted;
    [SerializeField] private bool ReturnFlag;
    [SerializeField] private bool CanReturn;
    [SerializeField]
    private bool DestoryAtEnd;
    private TimeRewinderV2 Tr2;

    // Start is called before the first frame update
    void Start()
<<<<<<< HEAD
    {if (GameObject.FindWithTag("Player_01").GetComponent<TimeRewinderV2>()) {
            Tr2 = GameObject.FindWithTag("Player_01").GetComponent<TimeRewinderV2>();

        }
        if (UseY)
=======
    {
        Tr2 = GameObject.FindWithTag("Player").GetComponent<TimeRewinderV2>();
        if(UseY)
>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
        {
            Current_Target = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            FirstPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            Current_Target = new Vector3(transform.position.x, 0f, transform.position.z);
            FirstPos = new Vector3(transform.position.x, 0f, transform.position.z);
        }
        LastPos = FirstPos;
        currentWayPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        if (GameObject.FindWithTag("Player_01").GetComponent<TimeRewinderV2>())
        {
            Tr2 = GameObject.FindWithTag("Player_01").GetComponent<TimeRewinderV2>();

        }if (Tr2 != null)
        {
            if (!Tr2.Isrewinding)
            {


                if (currentWayPoint < WayPoints.Count & CanMove)
                {
                    Current_Target = LastPos + WayPoints[currentWayPoint];
                    float step = MoveSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, Current_Target, step);

                    if (transform.position == Current_Target)
                    {
                        LastPos = transform.position;
                        currentWayPoint++;
                        if (currentWayPoint < WayPoints.Count)
                        {
                            Current_Target = WayPoints[currentWayPoint];
=======
        if (!Tr2.Isrewinding)
        {


            if (currentWayPoint < WayPoints.Count & CanMove)
            {
                Current_Target = LastPos + WayPoints[currentWayPoint];
                float step = MoveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, Current_Target, step);

                if (transform.position == Current_Target)
                {
                    LastPos = transform.position;
                    currentWayPoint++;
                    if (currentWayPoint < WayPoints.Count)
                    {
                        Current_Target = WayPoints[currentWayPoint];
                    }
                    else
                    {
                        // All waypoints reached, reset to initial position
                        if (DestoryAtEnd)
                        {
                            Destroy(gameObject);
                        }
                        else
                        {
                            if (CanReturn && ReturnFlag)
                            {
                                Current_Target = FirstPos;
                                currentWayPoint = 0;
                            }

>>>>>>> 6f84346c9bb722518521706b8623486b82fd633e
                        }
                        else
                        {
                            // All waypoints reached, reset to initial position
                            if (DestoryAtEnd)
                            {
                                Destroy(gameObject);
                            }
                            else
                            {
                                if (CanReturn && ReturnFlag)
                                {
                                    Current_Target = FirstPos;
                                    currentWayPoint = 0;
                                }

                            }

                        }
                    }
                }
            }
        }                                                                  
    }
    private void OnCollisionStay(Collision collision)
    {

        if (canBeInterupted)
        {
            CanMove = false;

        }

    }
    private void OnCollisionExit(Collision collision)
    {

        if (canBeInterupted)
        {
            CanMove = true;

        }
    }

}