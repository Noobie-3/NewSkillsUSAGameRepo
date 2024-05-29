using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDetection : MonoBehaviour
{
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
    public int EHp;
    public Animator Anim;
    public AudioSource SoundPlayer;
    public GameObject RootObectToDestroy;
    private bool DeathHappened;
    // Start is called before the first frame update
    void Start()
    {

        /*        HitBox = GC.Player.transform.Ta("HitBox").gameObject;*/
        if(Parent  != null)
        {
            Parent = transform.parent.gameObject;
        }
        OrigSize = gameObject.transform.parent.localScale;
        if (GameObject.FindWithTag("GC").GetComponent<GameController>())
        {
            GC = GameObject.FindWithTag("GC").GetComponent<GameController>();
        }
        /*        JumpKey = GC.Player.GetComponent<NewThirdPerson>().jumpKey;
         *        
        */
        EHp = eStats.EHp_Default;


    }

    // Update is called once per frame
    void Update()
    {


        if (TimeSquashed > 0 && isSquashed == true && GameController.instance.IsPaused == false)// decreasing the time till they can be swuashed again
        {
            TimeSquashed -= Time.deltaTime;

        }
        if(TimeSquashed <= 0 && isSquashed)
        {

            UnSqaush();
        }


        if (EHp <= 0)
        {
            Death();
        }


    }

    private void UnSqaush()
    {
        Anim.speed = 1;

        gameObject.transform.parent.localScale = new Vector3(gameObject.transform.parent.localScale.x, OrigSize.y, gameObject.transform.parent.localScale.z);
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
        if (CanBeHurt && GameController.instance.IsPaused == false)
        {
            if (other.gameObject == GC.Player)
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
            GC.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().Jump(BounceAmmount * BouceMulti);
            print("TestJump");

        }
        else
        {
            GC.Player.GetComponent<REVAMPEDPLAYERCONTROLLER>().Jump(BounceAmmount);
        }
    }

    private void Sqaush()
    {
        Anim.speed = 0;
            EHp--;
            gameObject.transform.parent.localScale = new Vector3(gameObject.transform.parent.localScale.x, OrigSize.y * .5f, gameObject.transform.parent.localScale.z);
            isSquashed = true;
            TimeSquashed = TimeToBeSquashed;
            CanBeHurt = false;

        Materials = gameObject.GetComponentInParent<Renderer>().materials;

        for (int i = 0; i < Materials.Length; i++)
        {
            Materials[i] = NewMat;
        }
        gameObject.GetComponentInParent<Renderer>().materials = Materials;

        if (SoundPlayer != null)
        {
            SoundPlayer.clip = eStats.HurtSound;
        }

        SoundPlayer.Play();

    }


    public void ParticleEffect(Transform Enemy)
    {
        Instantiate(eStats.KillPartcle, Enemy.position, Enemy.rotation);
    }

    public void Death()
    {if(DeathHappened == false)
        {
            DeathHappened = true;
            if (SoundPlayer != null)
            {
                SoundPlayer.clip = eStats.DeathSound;
                SoundPlayer.Play();
            }

            GameController.instance.GainCurrency(eStats.currencyGiven);
            ParticleEffect(gameObject.transform);
            int RandomNumber = UnityEngine.Random.Range(1, 100);
            if (RandomNumber <= eStats.ChanceToHeal && eStats.HealingItem != null)
            {
                Instantiate(eStats.HealingItem, transform.position, transform.rotation);
            }
            if (RootObectToDestroy != null)
            {
                Destroy(RootObectToDestroy, TimeSquashed);
            }

        }
        else
        {
            return;
        }
    }

}
