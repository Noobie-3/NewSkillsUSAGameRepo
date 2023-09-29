using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    //acsess to player
    public GameObject Player;
    private float XP = 0;
    public uint level = 1;//LEVEL UP STUFF
    public uint experienceToNextLevel = 100;


    public float TimeCooldown = 1;//TIMECONTROL and DAMAGE STUFF + HP
    public float PlayerMight = 1;
    public float PlayerDef = 1;
    public float HealingEffect = 1;
    public float PlayerHP = 3;
    public float PlayerMaxHP = 3;
    public bool IsInvincable = false;
    public float TimeTillDamageAgain = 2.0f;
    public float JumpForce;

    public float DefaultMoveSpeed = 10;//MOVEMENT VARS
    public float speed;



    public void addXP(float amount)//LEVEL UP AND XP FUNCTION
    {
        //add xp
        XP += amount;
        if (XP >= experienceToNextLevel)
        {
            //enough xp to level up
            level++;
            XP -= experienceToNextLevel;
            //adds more to the experience needed to level up (scaling)
            experienceToNextLevel += 10;
        }
    }

    public void DeathScene()
    {
        SceneManager.LoadScene("DeathScene");
    }

    public float TakeDamage(float damage, float HP, GameObject Target)
    {
        HP -= damage;
        print(HP);

        if(HP <= 0 && Target != Player)
        {
            Destroy(Target);
        }
        if (HP <= 0 && Target.tag == "Player")
        {
            DeathScene();
        }
        return HP;
    }

    private void Awake()
    {
        Cursor.visible = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        speed = DefaultMoveSpeed;
        PlayerHP = PlayerMaxHP;
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 45;
    }

    private void Update()
    {
        if (TimeTillDamageAgain > 0 )
        {
            IsInvincable = true;
            TimeTillDamageAgain -= Time.deltaTime;
        }
        else if(TimeTillDamageAgain < 0)
        {
            IsInvincable = false;
        }

        if (Player == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }




}

