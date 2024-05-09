using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//MAKE IT SWAP BACK TO NORMAL PHASE AFTER ENRAGE TIME

public class Boss_StateMachine : MonoBehaviour
{
    // Enum to define the phases of the boss
    public enum BossPhase
    {
        inactive,
        PhaseOne,
        PhaseTwo,
        PhaseThree,
        EnragedPhaseOne,
        EnragedPhaseTwo,
        Dead
    }

    // Current phase of the boss
    [Header("Boss Phase")]
    public BossPhase currentPhase = BossPhase.PhaseOne;

    // Reference to the boss stats scriptable object
    [Header("Boss Stats")]
    public Boss_stats stats;

    // Boss health
    [Header("Boss Health")]
    public float health;

    // Phase One variables
    [Header("Phase One")]
    public float phaseOneHealthThreshold = 2f;
    public float itemSpawnInterval = 3f;
    public float itemSpawnIntervalDefault = 3f;
    public GameObject itemPrefab;
    public Transform itemSpawnPoint;
    public float ItemSpawnTimer;

    // Phase Two variables
    [Header("Phase Two")]
    public float phaseTwoHealthThreshold = 1f;
    public float summonInterval = 5f;
    public float summonIntervalDefault = 5f;
    public GameObject enemyPrefab;
    public GameObject[] enemySpawnPoints;
    public float SummonTimer;
    public List<GameObject> enemyList;
    public int EnemyCount;
    public int MaxEnemies;
    public int DefaultMaxEnemies;
    public GameObject[] PosibleEnemies;
    public bool AllSpawned = false;
    public Animation RiseUpPlatform;
    public bool RoseUp;
    public GameObject[] MovingPlaforms;
    public GameObject ForceField;
    // Phase Three Variables
    [Header("Phase Three")]
    public float phaseThreeHealthThreshold = 0f;

    // Enrage Variables
    [Header("Enrage")]
    public float SpawnMultiplier = 2;
    [SerializeField] private float EnrageItemSpawnMultiplier;
    [SerializeField] private int EnrageEnemyMaxAmount;//The Max amopunt of enemies that can be spawned when enraged
    public float EnrageTime = 5;//how long bosss should be enraged
    public float enragedTimer = 0;//how long boss has been enraged
    public float TimeTillEnrage = 10;// current time till enraged
    public float TimeTakenTillEnrage; // How long it should take to get enraged
    public bool IsEnraged = false;
    // Enrage Bar UI
    public Image EnrageBar;
    public float EnrageAmount;

    //misc variables
    [Header("Miscellaneous")]
    public Animator animator;
    public bool IsInvulnerable;
    public float InInvulnerableTime = 2;
    public float InInvulnerableTimeDefault = 2;
    public  bool BossFightStarted;

    // Update is called once per frame
    private void Update()
    {

        IsInvunableChecker();

        // Update the enrage bar fill
        UpdateBar();
        // Switch statement to handle behavior based on current phase
        switch (currentPhase)
        {
            case BossPhase.inactive:
                InactivePhase();
                break;
            case BossPhase.PhaseOne:
                PhaseOne();
                break;
            case BossPhase.EnragedPhaseOne:
                EnragedPhaseOne();
                break;
            case BossPhase.PhaseTwo:
                PhaseTwo();
                break;
            case BossPhase.EnragedPhaseTwo:
                EnragedPhaseTwo();
                break;
            case BossPhase.PhaseThree:
                PhaseThree();
                break;
            case BossPhase.Dead:
                // Handle death state
                Dead();
                break;
        }
    }
    //Inactive Phase To Phase One

    private void InactivePhase()
    {

        if(BossFightStarted)
        {
            //i was goijng to do something but i  forgot So  come back to this i hate myself

            currentPhase = BossPhase.PhaseOne;
        }
    }


    // Behavior for Phase One
    private void PhaseOne()
    {
        // Increment timer
        itemSpawnInterval = itemSpawnIntervalDefault;
        ItemSpawnTimer += Time.deltaTime;
        RotateToLook();

        // Spawn item at regular intervals
        if (ItemSpawnTimer >= itemSpawnInterval)
        {
            SpawnItem();
            ItemSpawnTimer = 0f;
        }
        // Check if boss is enraged
        EnrageTracker();

        if(IsEnraged)//Transition to enraged phase one
        {
            currentPhase = BossPhase.EnragedPhaseOne;
        }
        // Transition to next Phase if health is below threshold
        if (HealthBelowThreshold(phaseOneHealthThreshold))
        {
            currentPhase = BossPhase.PhaseTwo;
            Debug.Log("Transitioning to Phase one enraged");
        }
    }

    // Behavior for Enraged Phase One
    private void EnragedPhaseOne()
    {
        // Modify spawn interval during enrage
        itemSpawnInterval = itemSpawnIntervalDefault / EnrageItemSpawnMultiplier;
        ItemSpawnTimer += Time.deltaTime;//PLEASE ADD WHERE YOU HAVE TO KILL ALL ENIMES  TO BE ABLE TO DAMAGE THE BOSS

        RotateToLook();

        // Spawn item at modified interval
        if (ItemSpawnTimer >= itemSpawnInterval)
        {
            SpawnItem();
            ItemSpawnTimer = 0f;
        }
        EnrageTracker();
        if(!IsEnraged)
        {
            currentPhase = BossPhase.PhaseOne;
            Debug.Log("Transitioning to Phase one");
        }
    }

    // Behavior for Phase Two
    private void PhaseTwo()
    {
        summonInterval = summonIntervalDefault;
        MaxEnemies = DefaultMaxEnemies;
        SummonTimer += Time.deltaTime;

        CapEnemies();
        // Spawn enemies at regular intervals
        if (SummonTimer >= summonInterval)
        {
            SummonEnemy();
        }

        // Check if boss is enraged
        EnrageTracker();

        if (IsEnraged)//Transition to enraged phase two
        {
            currentPhase = BossPhase.EnragedPhaseTwo;
        }

        // Transition to next Phase if health is below threshold
        if (HealthBelowThreshold(phaseTwoHealthThreshold))
        {
            currentPhase = BossPhase.PhaseThree;
            EnemyCount = 0;
            Debug.Log("Transitioning to Enraged Phase");
        }

    }

    // Behavior for Enraged Phase Two BOUNCE IDEAS OFF FOR ENRAGED FAZE TWO 
    private void EnragedPhaseTwo()
    {
        // Modify summon interval during enrage
        summonInterval = summonIntervalDefault / SpawnMultiplier;
        MaxEnemies = EnrageEnemyMaxAmount;
        SummonTimer += Time.deltaTime;

        CapEnemies();
        // Spawn enemies at regular intervals
        if (SummonTimer >= summonInterval && AllSpawned == false)
        {
            SummonEnemy();
        }
        EnrageTracker();
        if(!IsEnraged)
        {
            currentPhase = BossPhase.PhaseTwo;
            Debug.Log("Transitioning to Phase two");
        }
        // Additional behavior for Enraged Phase Two
    }



    // Behavior for Phase Three
    private void PhaseThree()
    {
        StartPlatforms();//Start moving the platforms for phase three
        
        CapEnemies();
        //Shoot and spawn enemies at regular intervals
        ItemSpawnTimer += Time.deltaTime;
        SummonTimer += Time.deltaTime;
        //rotate towards player
        RotateToLook();

        // Spawn item at regular intervals
        if (ItemSpawnTimer >= itemSpawnInterval)
        {
            SpawnItem();
            ItemSpawnTimer = 0f;
        }

        // Spawn enemies at regular intervals
        if (SummonTimer >= summonInterval)
        {
            SummonEnemy();
        }

        // Transition to Dead state if health is below threshold
        if (HealthBelowThreshold(phaseThreeHealthThreshold))
        {
            currentPhase = BossPhase.Dead;
            Debug.Log("Transitioning to Dead Phase");
        }
    }

    private void Dead()
    {
        // Play death animation
        //animator.SetTrigger("Death");
        //spin boss when dead
        for(int i = 0; i < enemyList.Count; i++)
        {
            Destroy(enemyList[i]);
        }
        transform.DORotate(new Vector3(0, 360, 0), 10, RotateMode.WorldAxisAdd);
        SpawnItem();
        // Destroy boss object
        Destroy(gameObject, 10);
    }


    // Method to spawn an item
    private void SpawnItem()
    {
        //Fire Randomly when dead also fire upwards
        if (currentPhase == BossPhase.Dead)
        {
            GameObject item = Instantiate(itemPrefab, itemSpawnPoint.position, Quaternion.identity);
            item.GetComponent<Bullet_Behavior>().stats = stats;
            Vector3 direction = new Vector3(1, Random.Range(.7f,10), 1);
            item.GetComponent<Rigidbody>().AddForce(direction * 10f, ForceMode.Impulse);
            return;
        }
        else
        {
            // Instantiate item and throw towards player
            GameObject item = Instantiate(itemPrefab, itemSpawnPoint.position, Quaternion.identity);
            item.GetComponent<Bullet_Behavior>().stats = stats;
            item.GetComponent<Bullet_Behavior>().CanDamage = true;
            Vector3 direction = (GameController.instance.Player.transform.position - transform.position).normalized;
            item.GetComponent<Rigidbody>().AddForce(direction * 10f, ForceMode.Impulse);
        }
    }


    // Method to rotate towards player
    private void RotateToLook()
    {
        // Rotate towards player after getting the player position
        GameObject target = GameController.instance.Player;
        Vector3 RotateDir = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        gameObject.transform.LookAt(RotateDir);
    }

    // Method to summon an enemy
    private void SummonEnemy()
    {
        // Randomly select a spawn point and instantiate enemy at that point if list has less than max enemies
        if(IsEnraged)
        {
            int SpawnPoinNum = Random.Range(0, enemySpawnPoints.Length);
            int RandomEnemy = Random.Range(0, PosibleEnemies.Length);

            enemyList.Add(Instantiate(PosibleEnemies[RandomEnemy], enemySpawnPoints[SpawnPoinNum].transform.position, Quaternion.identity));
            EnemyCount++;
            SummonTimer = 0f;

        }
        else if (SummonTimer >= summonInterval && AllSpawned == false )
        {
            int SpawnPoinNum = Random.Range(0, enemySpawnPoints.Length);
            enemyList.Add(Instantiate(enemyPrefab, enemySpawnPoints[SpawnPoinNum].transform.position, Quaternion.identity));
            EnemyCount++;
            SummonTimer = 0f;
        }
        else return;

    }

    // Method to check if health is below a specified threshold
    private bool HealthBelowThreshold(float threshold)
    {
        return health < threshold;
    }

    // Method to check if boss is defeated
    private bool IsDefeated()
    {
        return false; // Placeholder return
    }

    private void CapEnemies()
    {

        if (enemyList.Count != 0)
        {
            IsInvulnerable = true;
        }
        SummonTimer += Time.deltaTime;

        // Check if enemy list has any null entries and remove them
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null && AllSpawned)
            {
                enemyList.Remove(enemyList[i]);
                EnemyCount--;
            }


        }
        if (EnemyCount >= MaxEnemies)
        {
            AllSpawned = true;
        }
        else if (IsEnraged && EnemyCount < MaxEnemies)
        {
            AllSpawned = false; 


        }

    }


    private void EnrageTracker()
    {
        if(!IsEnraged)
        {
            TimeTillEnrage += Time.deltaTime;

        }
        else
        {
            enragedTimer += Time.deltaTime;
            IsInvulnerable = true;
        }
        if (TimeTillEnrage >= TimeTakenTillEnrage)
        {
            IsEnraged = true;
        }

        if (IsEnraged && enragedTimer >= EnrageTime)
        {
            IsEnraged = false;
            TimeTillEnrage = 0;//Make Bar go backwards instead of just reseting
            enragedTimer = 0;
        }
    }


    private void StartPlatforms()
    {
        if (!RoseUp)
        {
            if(RiseUpPlatform != null)
            {
                RiseUpPlatform.Play();

            }
            RoseUp = true;
        }
        
            for (int i = 0; i < MovingPlaforms.Length; i++)
            {
                if (MovingPlaforms[i].GetComponent<MoveAlongwayPoints>())
                {
                    if (MovingPlaforms[i].GetComponent<MoveAlongwayPoints>().CanMove == false)
                    {
                        MovingPlaforms[i].GetComponent<MoveAlongwayPoints>().CanMove = true;
                    }
                }

            }
        
    }


    // Update the bar fill based on the current value
    public void UpdateBar()
    {
        float fillAmount;

        if (!IsEnraged)
        {
            // Calculate fill amount based on the time remaining till enrage
            fillAmount = TimeTillEnrage / TimeTakenTillEnrage;
            EnrageAmount = EnrageTime;
        }
        else
        {
            EnrageAmount -= Time.deltaTime;
            //bar goes down with enrage amount
            fillAmount = EnrageAmount / EnrageTime;

        }

        // Set the fill amount of the bar
        EnrageBar.fillAmount = fillAmount;
    }
    private void IsInvunableChecker()
    {
        if(IsInvulnerable)
        {
            InInvulnerableTime -= Time.deltaTime;
        }

        if (ForceField.activeSelf != IsInvulnerable)
        {
            ForceField.SetActive(IsInvulnerable);
        }

        if (enemyList.Count != 0 || IsEnraged || InInvulnerableTime >= 0 && IsInvulnerable)
        {
            IsInvulnerable = true;
        }
        else
        {

            IsInvulnerable = false;
            InInvulnerableTime = InInvulnerableTimeDefault;

        }
    }
}
