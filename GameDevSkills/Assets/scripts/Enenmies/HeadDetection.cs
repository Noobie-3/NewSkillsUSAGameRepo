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
    public Material NewMat;
    public Material OldMat;
    public Material[] Materials;

    public Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        eStats = GetComponentInParent<Enemy_Stats>();
        if (GameObject.FindWithTag("Player_01").GetComponent<Rigidbody>())
        {
            rb = GameObject.FindWithTag("Player_01").GetComponent<Rigidbody>();
        }
        /*        HitBox = GC.Player.transform.Ta("HitBox").gameObject;*/
        Parent = transform.parent.gameObject;
        OrigSize = gameObject.transform.root.localScale;
        if (GameObject.FindWithTag("GC").GetComponent<GameController>())
        {
            GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
        }
/*        JumpKey = GC.Player.GetComponent<NewThirdPerson>().jumpKey;
 *        
*/    
    

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Player_01").GetComponent<Rigidbody>() != null && rb == null)
        {
            rb = GameObject.FindWithTag("Player_01").GetComponent<Rigidbody>();
        }
        if (GameObject.FindWithTag("GC").GetComponent<GameController>() != null && GC == null)
        {
            GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
        }
        if (TimeSquashed > 0 && isSquashed == true)// decreasing the time till they can be swuashed again
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
        Anim.speed = 1;

        gameObject.transform.root.localScale = new Vector3(gameObject.transform.root.localScale.x, OrigSize.y, gameObject.transform.root.localScale.z);
        isSquashed = false;
        CanBeHurt = true;
        Materials = gameObject.GetComponentInParent<Renderer>().materials;

        for (int i = 0; i < Materials.Length; i++)
        {
            Materials[i] = OldMat;
        }
        gameObject.GetComponentInParent<Renderer>().materials = Materials;

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
        Anim.speed = 0;
            eStats.EHp--;
            gameObject.transform.root.localScale = new Vector3(gameObject.transform.root.localScale.x, OrigSize.y * .5f, gameObject.transform.root.localScale.z);
            isSquashed = true;
            TimeSquashed = TimeToBeSquashed;
            CanBeHurt = false;

        Materials = gameObject.GetComponentInParent<Renderer>().materials;

        for (int i = 0; i < Materials.Length; i++)
        {
            Materials[i] = NewMat;
        }
        gameObject.GetComponentInParent<Renderer>().materials = Materials;
    }

}
