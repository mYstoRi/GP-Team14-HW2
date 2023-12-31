using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [Header("SpawnCount")]
    public int initialSpawnCount = 0; // how many enemys spawned when Start()
    public int spawnCount = 1; // how many enemys should be spawned every spawn
    public int spawnCountChanges = 0; // how spawnCount changes(+increase / -decrease) every spawn
    public int miniumspawnCount = 1; // lower bound of spawn count (negitive for not bounding)
    public int maxiumspawnCount = -1; // upper bound of spawn count (negitive for not bounding)


    [Header("SpawnInterval")]
    public float timeUntilSpawn = 0f; // time(in seconds) until next spawn
    public float spawnInterval = 1f; // time(in seconds) gap between every spawn
    public float spawnIntervalChanges = 0f; // how spawnInterval changes(in seconds)(+increase / -decrease) every spawn
    public float miniumSpawnInterval = 1f; // lower bound of spawn interval (negitive for not bounding)
    public float maxiumSpawnInterval = -1f; // lower bound of spawn interval (negitive for not bounding)


    private List<EnemySpawnPoint> enemySpawnPoints;
    // a collection of spawnPoints for future random picking
    // auto collected when Start() is called


    void Start()
    {
        // Collect all the enemy spawn points under this EnemySpawner
        enemySpawnPoints = new List<EnemySpawnPoint>();
        foreach(Transform child in this.transform)
        {
            if (child.tag == "EnemySpawnPoint")
            {
                enemySpawnPoints.Add(child.gameObject.GetComponent<EnemySpawnPoint>());
            }
        }

        Spawn(initialSpawnCount);
    }

    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;
        if(timeUntilSpawn <= 0) Spawn(spawnCount);
    }

    void Spawn(int count)
    {
        int weightSum = enemySpawnPoints.Aggregate(0, (acc, nxt)=>(acc+nxt.weight));
        for(int i = 0; i < count; i++)
        {
            int idx, rnd = Random.Range(0, weightSum);
            for(idx = 0; idx < enemySpawnPoints.Count && rnd > 0; idx++)
            {
                rnd -= enemySpawnPoints[idx].weight;
            }
            enemySpawnPoints[idx].Spawn();
        }
        timeUntilSpawn = spawnInterval; // update spawnTimeCounter
        UpdateSpawnCount();
        UpdateSpawnInterval();
    }

    void UpdateSpawnCount()
    {
        spawnCount += spawnCountChanges; // update spawnCount
        if(miniumspawnCount >= 0f) // negitive for not bounding
        {
            spawnCount = Mathf.Max(spawnCount, miniumspawnCount);
        }
        else if(maxiumspawnCount >= 0f) // negitive for not bounding
        {
            spawnCount = Mathf.Min(spawnCount, maxiumspawnCount);
        }
    }

    void UpdateSpawnInterval()
    {
        spawnInterval += spawnIntervalChanges; // update spawnInterval
        if(miniumSpawnInterval >= 0f) // negitive for not bounding
        {
            spawnInterval = Mathf.Max(spawnInterval, miniumSpawnInterval);
        }
        else if(maxiumSpawnInterval >= 0f) // negitive for not bounding
        {
            spawnInterval = Mathf.Min(spawnInterval, maxiumSpawnInterval);
        }
    }
}
