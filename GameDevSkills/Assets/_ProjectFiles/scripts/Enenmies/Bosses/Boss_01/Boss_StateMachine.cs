    using UnityEngine;

public class Boss_StateMachine : MonoBehaviour
{
    public enum BossPhase
    {
        PhaseOne,
        PhaseTwo,
        Enraged,
        Dead
    }

    public BossPhase currentPhase = BossPhase.PhaseOne;
    public Boss_stats stats;

    // Phase One variables
    public float phaseOneHealthThreshold = 2f;
    public float itemSpawnInterval = 3f;
    public GameObject itemPrefab;
    public Transform itemSpawnPoint;

    // Phase Two variables
    public float phaseTwoHealthThreshold = 1f;
    public float summonInterval = 5f;
    public GameObject enemyPrefab;
    public GameObject[] enemySpawnPoints;


    private float timer;



    private void Update()
    {
        switch (currentPhase)
        {
            case BossPhase.PhaseOne:
                PhaseOne();
                break;
            case BossPhase.PhaseTwo:
                PhaseTwo();
                break;
            case BossPhase.Enraged:
                Enraged();
                break;
            case BossPhase.Dead:
                // Handle death state
                break;
        }
    }

    private void PhaseOne()
    {
        timer += Time.deltaTime;
        gameObject.transform.LookAt(GameController.instance.Player.transform.position);
        // Phase One behavior
        if (timer >= itemSpawnInterval)
        {
            SpawnItem(); //Works
            timer = 0f;
        }

        // Transition to Phase Two
        if (HealthBelowThreshold(phaseOneHealthThreshold))
        {
            currentPhase = BossPhase.PhaseTwo;
            Debug.Log("Transitioning to Phase Two");
        }
    }

    private void PhaseTwo()
    {
        timer += Time.deltaTime;

        // Phase Two behavior
        if (timer >= summonInterval)
        {
            SummonEnemy();
            timer = 0f;
        }

        // Transition to Enraged
        if (HealthBelowThreshold(phaseTwoHealthThreshold))
        {
            currentPhase = BossPhase.Enraged;
            Debug.Log("Transitioning to Enraged Phase");
        }
    }

    private void Enraged()
    {
        // Enraged behavior
        // Add more aggressive attacks or behaviors

        // Handle defeat condition
        if (IsDefeated())
        {
            currentPhase = BossPhase.Dead;
            Debug.Log("Boss defeated");
        }
    }

    private void SpawnItem()
    {
        // Instantiate item and throw towards player
        GameObject item = Instantiate(itemPrefab, itemSpawnPoint.position, Quaternion.identity);
        item.GetComponent<Bullet_Behavior>().stats = stats;
        Vector3 direction = (GameController.instance.Player.transform .position - transform.position).normalized;
        item.GetComponent<Rigidbody>().AddForce(direction * 10f, ForceMode.Impulse);
    }

    private void SummonEnemy()
    {

        int RandomNum = Random.Range(0, enemySpawnPoints.Length);
        // Instantiate enemy
        Instantiate(enemyPrefab, enemySpawnPoints[RandomNum].transform.position, Quaternion.identity);
    }

    private bool HealthBelowThreshold(float threshold)
    {
        // Check if boss's health is below a certain threshold
        // Implement your own health management system here
        return false; // Placeholder return
    }

    private bool IsDefeated()
    {
        // Check if boss is defeated
        // Implement your own defeat condition here
        return false; // Placeholder return
    }
}
