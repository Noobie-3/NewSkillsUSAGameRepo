using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SlidingDoorTImeControlled : MonoBehaviour
{
    public Vector3 StartPosition;
    public Vector3 targetPosition;
    public Vector3 Disance;
    public float DisanceLeft;
    public float MoveSpeed;
    public Button Button;
    public  bool IsMoving;

    private void Awake()
    {
        StartPosition = gameObject.transform.position;

    }
    private void Start()
    {

        targetPosition = StartPosition + Disance;
        
    }



    public void Update()
    {
        if(Button.IsPressed)
        {
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
        if (IsMoving)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        DisanceLeft = Vector3.Distance(transform.position, targetPosition);



        if (DisanceLeft > 0)
        {
            /*            gameObject.transform.Translate(targetPosition * MoveSpeed * Time.deltaTime );
            */
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, MoveSpeed);
        }


    }

    private void CloseDoor() {

        if (IsMoving == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, StartPosition, MoveSpeed * 2.8f);
            print("hello");

        }
    }
}
