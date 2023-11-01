using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    // One EnemySpawnPoint is responsable for spawning only One Type of Enemy
    public GameObject enemyPrefab;

    /* TODO:

    // this weight should affect [EnemySpawner]'s Randomness, higher weight -> higher rate of being chosen to spawn enemy, vise versa.
    public int weight = 1; 

    // change weight over time to make weak enemy spawn less over time.
    public int weightChanges = 0;

    */


    public void Spawn()
    {
        Debug.Log("Spawn Called");
        // Spawn Enemy here
    }
}
