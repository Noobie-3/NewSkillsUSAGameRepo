using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongwayPoints : MonoBehaviour
{
    public List<Vector3> WayPoints;
    public float MoveSpeed;
    public Vector3 Current_Target;
    public Vector3 FirstPos;
    public bool IsMoving;
    public int currentWayPoint;

    // Start is called before the first frame update
    void Start()
    {
        Current_Target = new Vector3 (transform.position.x, 0f, transform.position.z);   
        FirstPos = new Vector3 (transform.position.x, 0f, transform.position.z);   
        currentWayPoint = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWayPoint < WayPoints.Count)
        {
            float step = MoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Current_Target, step);

            if (transform.position == Current_Target)
            {
                currentWayPoint++;
                if (currentWayPoint < WayPoints.Count)
                {
                    Current_Target = WayPoints[currentWayPoint];
                }
                else
                {
                    // All waypoints reached, reset to initial position

                    Current_Target = FirstPos;
                    currentWayPoint = 0;
                }
            }
        }
    }

}

