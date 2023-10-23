using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemySpawner : MonoBehaviour
{
    public int Frequency; //IN Seconds
    public int MaxEnemies;
    public List<GameObject> AllEnemiesSpawned;
    public GameObject EnemyPrefab;
    public float Cooldown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Cooldown < Frequency )
        {
            Cooldown += Time.deltaTime;
            print("Added_Time");
        }
        
        if(Cooldown > Frequency && AllEnemiesSpawned.Count < MaxEnemies)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        
         GameObject LocalEnemy = Instantiate(EnemyPrefab, transform.position, transform.rotation);

        AllEnemiesSpawned.Insert(0, LocalEnemy);
        Cooldown = 0;
    }
}
