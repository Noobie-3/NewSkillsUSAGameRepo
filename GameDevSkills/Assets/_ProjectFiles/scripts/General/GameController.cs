using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    //self reference
    public static GameController instance;

    // Access to player GameObject
    public GameObject Player;
    
    // Experience and level variables
    private float XP = 0;
    public uint level = 1;
    public uint experienceToNextLevel = 100;

    // Player attributes
    public float TimeCooldown = 1;
    public bool Canrewind;
    public float PlayerMight = 1;
    public float PlayerDef = 1;
    public float HealingEffect = 1;
    public float PlayerHP = 3;
    public float PlayerMaxHP = 3;
    public bool IsInvincable = false;
    public float TimeTillDamageAgain = 2.0f;
    public float hurt_Time_Default;
    public float JumpForce;
    public bool CanRewind;
    public int Current_Currency = 0;
    public bool isDead;
    public bool IsPaused = false;
    internal bool isRewinding;
    internal float maxRecordingDuration;
    public AudioSource HurtSoundForPlayer;

    // Materials for player object
    public Material[] Materials;
    public Material OldMat;

    // Function to add experience points
    public void addXP(float amount)
    {
        XP += amount;
        if (XP >= experienceToNextLevel)
        {
            level++;
            XP -= experienceToNextLevel;
            experienceToNextLevel += 10; // Scaling experience needed to level up
        }
    }

    // Function to handle player death
    public void DeathScene()
    {
        isDead = true;
    }

    // Function to handle taking damage without particle effect
    public float TakeDamage(float damage, float HP, GameObject Target)
    {
        HP -= damage;
        if (HP <= 0 && Target != Player)
        {
            Destroy(Target);
        }
        if (HP <= 0 && Target.tag == "Player_01")
        {
            DeathScene();
        }
        return HP;
    }

    // Function to handle taking damage with particle effect
    public float TakeDamage(float damage, float HP, GameObject Target, GameObject Particle_effect_Death)
    {
        HP -= damage;
        if (HP <= 0 && Target != Player)
        {
            Instantiate(Particle_effect_Death, Target.transform.position, Quaternion.identity);
            Destroy(Target, 1);
            Destroy(Particle_effect_Death, 1);
        }
        if (HP <= 0 && Target.tag == "Player_01")
        {
            DeathScene();
        }
        return HP;
    }

    // Awake function is called when the script instance is being loaded
    private void Awake()
    {
        OldMat = Player.gameObject.GetComponentInChildren<Renderer>().materials[0];
        DontDestroyOnLoad(gameObject);
        PlayerHP = PlayerMaxHP;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDead)
        {
            IsPaused = true;
        }
        if (TimeTillDamageAgain > 0 && !IsPaused)
        {
            IsInvincable = true;
            TimeTillDamageAgain -= Time.deltaTime;
        }
        else if (TimeTillDamageAgain < 0)
        {
            IsInvincable = false;
            MatChange(OldMat);
        }
        if (IsPaused)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
        if (Player == null && GameObject.FindGameObjectWithTag("Player_01") != null)
        {
            Player = GameObject.FindGameObjectWithTag("Player_01");
        }
    }

    // Function to gain currency
    public void GainCurrency(int Amount)
    {
        Current_Currency += Amount;
    }

    // Function to change material
    private void MatChange(Material Mat)
    {
        Materials = GameController.instance.Player.gameObject.GetComponentInChildren<Renderer>().materials;
        if (Materials != null)
        {
            for (int i = 0; i < Materials.Length; i++)
            {
                Materials[i] = Mat;
            }
            GameController.instance.Player.gameObject.GetComponentInChildren<Renderer>().materials = Materials;
        }
    }
}
