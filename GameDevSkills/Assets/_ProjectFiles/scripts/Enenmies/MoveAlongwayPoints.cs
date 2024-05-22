using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongwayPoints : MonoBehaviour
{
    public List<Vector3> WayPoints;
    public float MoveSpeed;
    public Vector3 Current_Target;
    private Vector3 FirstPos;
    public Vector3 LastPos;
    private bool IsMoving = true;
    public int currentWayPoint;
    public bool UseY;
    public bool CanMove;
    public bool canBeInterupted;
    [SerializeField] private bool ReturnFlag;
    [SerializeField] public bool CanReturn;
    public bool StayAfterReturning;
    [SerializeField]
    private bool DestoryAtEnd;
    private TimeRewinderV2 Tr2;

    // Start is called before the first frame update
    void Start()
    {if (GameObject.FindWithTag("Player_01").GetComponent<TimeRewinderV2>()) {
            Tr2 = GameObject.FindWithTag("Player_01").GetComponent<TimeRewinderV2>();

        }
        if (UseY)

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
        if (GameController.instance.Player.GetComponent<TimeRewinderV2>())
        {
            Tr2 = GameController.instance.Player.GetComponent<TimeRewinderV2>();

        }if (Tr2 != null)
        {
            if (!Tr2.Isrewinding)
            {


                if (currentWayPoint < WayPoints.Count & CanMove)
                {
                    Current_Target = WayPoints[currentWayPoint];
                    float step = MoveSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, Current_Target, step);

                    if (transform.position == Current_Target)
                    {
                        if(StayAfterReturning && CanReturn && Current_Target == WayPoints[0])
                        {
                            CanMove = false;
                        }
                        else
                        {
                            LastPos = transform.position;
                            currentWayPoint++;

                        }
                    }
                }
                if (currentWayPoint < WayPoints.Count )
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
    public void AddNewWayPoint() {
    
        WayPoints.Add (new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));


    
    }
}