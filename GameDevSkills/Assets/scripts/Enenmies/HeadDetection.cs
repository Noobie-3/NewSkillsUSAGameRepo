using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDetection : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject HitBox;
    public Enemy_Stats eStats;
    public GameController GC;
    public GameObject Parent;
    public float TimeSquashed;
    public float TimeToBeSquashed;
    public bool isSquashed;
    Vector3 OrigSize;
    public float BounceAmmount;
    private bool CanBeHurt = true;
    KeyCode JumpKey;
    public float BouceMulti;

    // Start is called before the first frame update
    void Start()
    {
        eStats = GetComponentInParent<Enemy_Stats>();
        rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
/*        HitBox = GC.Player.transform.Ta("HitBox").gameObject;*/
        Parent = transform.parent.gameObject;
        OrigSize = Parent.transform.localScale;
        GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
        JumpKey = GC.Player.GetComponent<NewThirdPerson>().jumpKey;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeSquashed > 0 && isSquashed == true)// decreasing the time till they can be swuashed again
        {
            TimeSquashed -= Time.deltaTime;

        }
        if(TimeSquashed <= 0 && isSquashed)
        {

            UnSqaush();
        }



    }

    private void UnSqaush()
    {
        Parent.transform.localScale = new Vector3(Parent.transform.localScale.x, OrigSize.y, Parent.transform.localScale.z);
        isSquashed = false;
        CanBeHurt = true;
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (CanBeHurt)
        {
            if (other.gameObject == GC.Player && rb.velocity.y < 0)
            {
                Sqaush();
                Bounce();
            }
        }
    }

    private void Bounce()
    {
        if (Input.GetKey(JumpKey))
        {
            GC.Player.GetComponent<NewThirdPerson>().Jump(BounceAmmount * BouceMulti);
            print("TestJump");

        }
        else
        {
            GC.Player.GetComponent<NewThirdPerson>().Jump(BounceAmmount);
        }
    }

    private void Sqaush()
    {

            eStats.EHp--;
            Parent.transform.localScale = new Vector3(Parent.transform.localScale.x, OrigSize.y * .5f, Parent.transform.localScale.z);
            isSquashed = true;
            TimeSquashed = TimeToBeSquashed;
            CanBeHurt = false;

    }

}
