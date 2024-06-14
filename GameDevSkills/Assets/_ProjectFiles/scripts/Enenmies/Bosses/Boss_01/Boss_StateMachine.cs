using DG.Tweening;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private BossPhase currentPhase = BossPhase.PhaseOne;

    // Reference to the boss stats scriptable object
    [SerializeField] private Boss_stats stats;

    // Boss health
    [SerializeField] public float health;

    // Phase One variables
    [Header("Phase One")]
    [SerializeField] private float phaseOneHealthThreshold = 2f;
    [SerializeField] private float itemSpawnInterval = 3f;
    [SerializeField] private float itemSpawnIntervalDefault = 3f;
    [SerializeField] private float Throwable_Speed = 10;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform[] ItemSpawnPoints;
    [SerializeField] private float ItemSpawnTimer;
    [SerializeField] private List<GameObject> ItemsSpawned;
    [SerializeField] private bool CanSpawn;
    [SerializeField] private GameObject target;
    [SerializeField] private bool ThrowLeft;


    // Phase Two variables
    [Header("Phase Two")]
    [SerializeField] private float phaseTwoHealthThreshold = 1f;
    [SerializeField] private float summonInterval = 5f;
    [SerializeField] private float summonIntervalDefault = 5f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] enemySpawnPoints;
    [SerializeField] private float SummonTimer;
    [SerializeField] private List<GameObject> enemyList;
    [SerializeField] private int EnemyCount;
    [SerializeField] private int MaxEnemies;
    [SerializeField] private int DefaultMaxEnemies;
    [SerializeField] private GameObject[] PosibleEnemies;
    [SerializeField] public bool AllSpawned = false;
    [SerializeField] private Animation RiseUpPlatform;
    [SerializeField] private bool RoseUp;
    [SerializeField] private GameObject[] MovingPlaforms;
    [SerializeField] private GameObject ForceField;
    [SerializeField] private bool CanSummon;

    // Phase Three Variables
    [Header("Phase Three")]
    [SerializeField] private float phaseThreeHealthThreshold = 0f;

    // Enrage Variables
    [Header("Enrage")]
    [SerializeField] private float SpawnMultiplier = 2;
    [SerializeField] private float SummonTime_Enraged = 2;

    [SerializeField] private float EnrageItemSpawnMultiplier;
    [SerializeField] private int EnrageEnemyMaxAmount;
    [SerializeField] private float EnrageTime = 5;
    [SerializeField] private float enragedTimer = 0;
    [SerializeField] private float TimeTillEnrage = 10;
    [SerializeField] private float TimeTakenTillEnrage;
    [SerializeField] private bool IsEnraged = false;
    [SerializeField] private float EnrageAnimTime;
    // Enrage Bar UI
    [SerializeField] private Image EnrageBar;
    [SerializeField] private float EnrageAmount;

    //misc variables
    [Header("Miscellaneous")]
    [SerializeField] private Animator anim;

    [SerializeField] public bool IsInvulnerable;
    [SerializeField] public float InInvulnerableTime = 2;
    [SerializeField] public float InInvulnerableTimeDefault = 2;
    [SerializeField] public bool BossFightStarted;


    // Update is called once per frame
    private void Update()
    {
        if(!GameController.instance.IsPaused)
        {

        
        if(target == null && GameController.instance.Player != null)
        {
            target = GameController.instance.Player;
        }
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
    }
    //Inactive Phase To Phase One

    private void InactivePhase()
    {

        if (BossFightStarted)
        {
            //Start Boss music
            // Shut door
            //center platform drops to make fighting arena bigger

            // Transition to Phase One when the boss fight starts
            currentPhase = BossPhase.PhaseOne;
        }
    }


    // Behavior for Phase One
    private void PhaseOne()
    {
        // Reset item spawn interval to default value
        itemSpawnInterval = itemSpawnIntervalDefault;
        // Rotate towards player and update attack timers
        RotateToLook();
        CanAttackTimerUpdater();
        // Check if boss is enraged
        EnrageTracker();

        if (IsEnraged)//Transition to enraged phase one
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
        CanAttackTimerUpdater();
        RotateToLook();
        EnrageTracker();
        CanAttackTimerUpdater();
        if (!IsEnraged)
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
        CanAttackTimerUpdater();
        CapEnemies();
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
        summonInterval = SummonTime_Enraged;
        CanAttackTimerUpdater();
        MaxEnemies = EnrageEnemyMaxAmount;
        SummonTimer += Time.deltaTime;

        CapEnemies();
        CanAttackTimerUpdater();
        EnrageTracker();
        if (!IsEnraged)
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
        EnrageTracker();
        CapEnemies();
        //Shoot and spawn enemies at regular intervals
        CanAttackTimerUpdater();
        //rotate towards player
        RotateToLook();
        CanAttackTimerUpdater();
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
        for (int i = 0; i < enemyList.Count; i++)
        {
            Destroy(enemyList[i]);
        }
        transform.DORotate(new Vector3(0, 360, 0), 10, RotateMode.WorldAxisAdd);
        CanSpawn = true;
       // SpawnItem();

        // Destroy boss object
        Destroy(gameObject, 10);
    }


    // Method to spawn an item
    public void SpawnItem()
    {
        if (CanSpawn)
        {
            int CurrentSpawnPoint;

            if (ThrowLeft)
            {
                CurrentSpawnPoint = 1;
/*                ThrowLeft = !ThrowLeft;
*/            }
            else
            {
                CurrentSpawnPoint = 0;
                ThrowLeft = !ThrowLeft;
            }
            // Fire Randomly when dead also fire upwards
            if (currentPhase == BossPhase.Dead)
                {
                    GameObject item = Instantiate(itemPrefab, ItemSpawnPoints[CurrentSpawnPoint].position, Quaternion.identity);
                    item.GetComponent<Bullet_Behavior>().stats = stats;
                    Vector3 direction = new Vector3(1, UnityEngine.Random.Range(.7f, 10), 1);
                    item.GetComponent<Rigidbody>().AddForce(direction * 10f, ForceMode.Impulse);
                    return;
                }
                else
                {
                    // Instantiate item and throw towards player
                    GameObject item = Instantiate(itemPrefab, ItemSpawnPoints[CurrentSpawnPoint].position, ItemSpawnPoints[CurrentSpawnPoint].transform.rotation);
                    item.gameObject.transform.SetParent(ItemSpawnPoints[CurrentSpawnPoint].gameObject.transform, true);
                    item.GetComponent<Bullet_Behavior>().stats = stats;
                    item.GetComponent<Bullet_Behavior>().CanDamage = true;
                    ItemsSpawned.Add(item);
                    if (anim != null)
                    {
                        anim.SetBool("CanSpawn", false);
                    }
                    for (int i = 0; i < ItemsSpawned.Count; i++)
                    {
                        if (ItemsSpawned[i] == null)
                        {
                            ItemsSpawned.Remove(ItemsSpawned[i]);
                        }
                    }
                    ItemsSpawned.Add(item);

                }
            }

        }


    public void AddVelToItem()
    {
        foreach (GameObject item in ItemsSpawned)
        {
            if (item != null)
            {
                item.transform.SetParent(null, true);
                if (!item.GetComponent<Bullet_Behavior>().hasBeenShot)
                {
                    /*                    if (ThrowLeft)
                                        {
                                            item.transform.position = ItemSpawnPoints[1].position;
                                        }
                                        else
                                        {
                                            item.transform.position = ItemSpawnPoints[0].position;
                                        }*/

/*                    item.transform.rotation = Quaternion.identity; // Reset rotation to avoid weird angles
*/
                    // Calculate direction towards target
                    Vector3 direction = (target.transform.position - item.transform.position).normalized;

                    // Debugging: Log the calculated direction
                    Debug.Log($"Direction: {direction}");

                    // Apply force
                    item.GetComponent<Rigidbody>().velocity = direction * Throwable_Speed;

                    item.GetComponent<Bullet_Behavior>().hasBeenShot = true;
                }
            }
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
    {if(CanSummon)
        {

            
            // Randomly select a spawn point and instantiate enemy at that point if list has less than max enemies
            if (IsEnraged)
            {
                int SpawnPoinNum = UnityEngine.Random.Range(0, enemySpawnPoints.Length);
                int RandomEnemy = UnityEngine.Random.Range(0, PosibleEnemies.Length);
                MaxEnemies = EnrageEnemyMaxAmount;
                enemyList.Add(Instantiate(PosibleEnemies[RandomEnemy], enemySpawnPoints[SpawnPoinNum].transform.position, Quaternion.identity));
                EnemyCount++;
            }
            else if (AllSpawned == false)
            {
                MaxEnemies = DefaultMaxEnemies;
                int SpawnPoinNum = UnityEngine.Random.Range(0, enemySpawnPoints.Length);
                enemyList.Add(Instantiate(enemyPrefab, enemySpawnPoints[SpawnPoinNum].transform.position, Quaternion.identity));
                EnemyCount++;

            }
        }
        else return;

    }

    // Method to check if health is below a specified threshold
    private bool HealthBelowThreshold(float threshold)
    {
        return health <= threshold;
    }
    private void  ResetCanSummon()
    {
        anim.SetBool("CanSummon", false);
        SummonTimer = 0;

    }

    // Method to check if boss is defeated
    private bool IsDefeated()
    {
        return false; // Placeholder return
    }

    // Cap the number of enemies spawned
    private void CapEnemies()
    {



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


    // Track the enrage state of the boss
    private void EnrageTracker()
    {
        if (!IsEnraged)
        {
            TimeTillEnrage += Time.deltaTime;

            if (anim != null && anim.speed != 1)
            {
                anim.speed = 1;
                print(anim.speed);
            }
        }
        else
        {
            enragedTimer += Time.deltaTime;
            if (anim != null && anim.speed != EnrageAnimTime)
            {
                anim.speed = EnrageAnimTime;
                print(anim.speed);

            }
        }
        if (TimeTillEnrage >= TimeTakenTillEnrage)
        {
            IsEnraged = true;
        }

        if (IsEnraged && enragedTimer >= EnrageTime)
        {
            IsEnraged = false;
            TimeTillEnrage = 0; // Reset time till enrage
            enragedTimer = 0;
        }
    }


    // Start moving platforms for phase three
    private void StartPlatforms()
    {
        if (!RoseUp)
        {
            if (RiseUpPlatform != null)
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


    // Update the enrage bar fill based on the current value
    public void UpdateBar()
    {
        if (EnrageBar != null)
        {
            if (BossFightStarted)
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
                    // Reduce enrage amount over time
                    EnrageAmount -= Time.deltaTime;
                    // Calculate fill amount based on the enrage amount
                    fillAmount = EnrageAmount / EnrageTime;

                }

                // Set the fill amount of the enrage bar
                EnrageBar.fillAmount = fillAmount;
            }

        }
        else if (EnrageBar == null && GameController.instance.BossBar != null)
        {
            EnrageBar = GameController.instance.BossBar;
        }
    }

    // Check if boss is invulnerable
    private void IsInvunableChecker()
    {
        if (IsInvulnerable)
        {
            InInvulnerableTime -= Time.deltaTime;
        }

        if (ForceField != null && ForceField.activeSelf != IsInvulnerable)
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

    // Add animations to the Boss
    private void Animate(string BoolToSet, bool state)
    {
        if (anim != null)
        {
            anim.SetBool(BoolToSet, state);
        }
    }

    // Update timers for attack actions
    private void CanAttackTimerUpdater()
    {
        // Update summon timer
        if (SummonTimer < summonInterval && !AllSpawned && !CanSummon && currentPhase != BossPhase.PhaseOne && currentPhase != BossPhase.EnragedPhaseOne)
        {
            SummonTimer += Time.deltaTime;

        }
        else if (!AllSpawned && SummonTimer >= summonInterval)
        {
            SummonTimer = 0;
            anim.SetBool("CanSummon", true);//fix it summoning enemies when all spawned is true;
        }


        // Update item spawn timer
        if (ItemSpawnTimer < itemSpawnInterval)
        {
            ItemSpawnTimer += Time.deltaTime;

        }
        else if (currentPhase == BossPhase.PhaseOne || currentPhase == BossPhase.EnragedPhaseOne || currentPhase == BossPhase.PhaseThree)
        {
            ItemSpawnTimer = 0;
            anim.SetBool("CanSpawn", true);
            print("this should fire");

        }
        CanSpawn = anim.GetBool("CanSpawn");
        CanSummon = anim.GetBool("CanSummon");
    }
}
