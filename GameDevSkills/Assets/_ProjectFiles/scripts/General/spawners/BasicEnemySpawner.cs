using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicEnemySpawner : MonoBehaviour
{
    public int Frequency; //IN Seconds
    public int MaxEnemies;
    public List<GameObject> AllEnemiesSpawned;
    public List<GameObject> EnemyPrefab;
    public float Cooldown;
    public  Vector2 radius;
    private Vector3 centerOfRadius;

    // Start is called before the first frame update
    void Start()
    {
        centerOfRadius = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < AllEnemiesSpawned.Count; i++)
        {
            if (AllEnemiesSpawned[i] == null)
            {
                AllEnemiesSpawned.Remove(AllEnemiesSpawned[i]);
            }
        }
        if(Cooldown < Frequency && AllEnemiesSpawned.Count < MaxEnemies )
        {
            Cooldown += Time.deltaTime;
        }
        
        if(Cooldown > Frequency && AllEnemiesSpawned.Count < MaxEnemies)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
                Vector3 target = centerOfRadius + (Vector3)(radius * UnityEngine.Random.insideUnitCircle);
        int RandomEnemy = UnityEngine.Random.Range(0, EnemyPrefab.Count);
         GameObject LocalEnemy = Instantiate(EnemyPrefab[RandomEnemy], new Vector3(target.x, transform.position.y, target.y), transform.rotation);

        AllEnemiesSpawned.Insert(0, LocalEnemy);
        Cooldown = 0;
    }
}
