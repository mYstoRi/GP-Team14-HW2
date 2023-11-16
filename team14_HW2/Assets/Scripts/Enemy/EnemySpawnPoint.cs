using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    // One EnemySpawnPoint is responsable for spawning only One Type of Enemy
    public List<GameObject> enemyPrefabs;

    public int weight = 1; // weight of spawning this spawner
    public int weightChanges = 0; // how weight changes(+increase / -decrease) every spawn
    public int miniumWeight = 1; // lower bound of weight  (negitive for not bounding)
    public int maxiumWeight = -1; // lower bound of weight  (negitive for not bounding)

    private GameObject prefab;

    void Start()
    {
        enemyPrefabs.RemoveAll(x => x==null); // remove null prefab;
    }

    public void Spawn()
    {
        if(enemyPrefabs.Count > 0)
        {
            UpdateWeight();
            prefab = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], transform.position, Quaternion.identity, transform); // Spawn Enemy here
            prefab.tag = "Enemy";
        }
        else Debug.LogWarning("Spawning a EnemySpawnPoint with no enemy prefab!");
    }

    void UpdateWeight()
    {
        weight += weightChanges; // update weight
        if(miniumWeight >= 0f) // negitive for not bounding
        {
            weight = Mathf.Max(weight, miniumWeight);
        }
        else if(maxiumWeight >= 0f) // negitive for not bounding
        {
            weight = Mathf.Min(weight, maxiumWeight);
        }
    }
}
