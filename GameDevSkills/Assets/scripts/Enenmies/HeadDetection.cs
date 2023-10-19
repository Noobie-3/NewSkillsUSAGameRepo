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

    // Start is called before the first frame update
    void Start()
    {
        eStats = GetComponentInParent<Enemy_Stats>();
        rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
/*        HitBox = GC.Player.transform.Ta("HitBox").gameObject;*/
        Parent = transform.parent.gameObject;
        OrigSize = Parent.transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        if(TimeSquashed > 0 && isSquashed == true)
        {
            TimeSquashed -= Time.deltaTime;
        }
        if(isSquashed)
        {

            Sqaush();
        }
        else
        {
            Parent.transform.localScale = OrigSize;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HitBox" && rb.velocity.y < 0 && other.transform.parent == GC.Player)
        {
            Sqaush();
        }
    }

    private void Sqaush()
    {
        print("Swuashed");
        Parent.transform.localScale = new Vector3(Parent.transform.localScale.x, Parent.transform.localScale.y * .5f, Parent.transform.localScale.z);
        isSquashed = true;
        TimeSquashed = TimeToBeSquashed;
    }
}
