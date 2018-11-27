using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 
/// </summary>
public class EnemySpawn : NetworkBehaviour
{
    public int currentEnemyCount = 0;
    public int maxEnemyLimit = 3;

    [Tooltip("Prefab of the enemy.")]
    public GameObject enemy;

    [Tooltip("The time between spawning an enemy.")]
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    private bool _enableSpawning;


    void OnEnable()
    {
        _enableSpawning = false;
    }

    public override void OnStartServer()
    {
        _enableSpawning = true;
        maxEnemyLimit = GameManager.instance.maximumEnemyLimit;
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
    }

    void ResetData()
    {
        _enableSpawning = false;
        currentEnemyCount = 0;
    }


    /// <summary>
    /// 
    /// </summary>
    void SpawnEnemy()
    {
        // Check to see if the server is active
        // and spawn the enemy once server is active
        if (NetworkServer.active)
        {
            if (_enableSpawning)
            {
                if (spawnPoints.Length <= 0)
                { // If no spawn points were given, then do nothing
                    Debug.LogError("[EnemySpawn]: Please assign spawn points.");
                    return;
                }
                if (currentEnemyCount >= maxEnemyLimit)
                {
                    return;
                }

                // Create an enemy at a random spawn point
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);

                // Create an instance of the enemy and spawn at the points
                GameObject enemyObject = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                ++currentEnemyCount;

                NetworkServer.Spawn(enemyObject);
            }
        }
        else
        {
            ResetData();
        }
    }
}
