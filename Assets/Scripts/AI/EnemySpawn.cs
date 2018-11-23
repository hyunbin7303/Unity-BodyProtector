using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class EnemySpawn : MonoBehaviour
{
    public int currentEnemyCount = 0;
    public int maxEnemyLimit = 3;

    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        if (spawnPoints.Length <= 0) {
            return;
        }
        if (currentEnemyCount >= maxEnemyLimit) {
            return;
        }

        // Create an enemy at a random spawn point
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemey and spawn at the points
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        ++currentEnemyCount;
    }
}
