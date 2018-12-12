using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// EnemySpawn class contains game logic to spawn enemies at random locations.
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


    public override void OnStartServer()
    {
        StartCoroutine("SpawnEnemy");
    }

    void Start()
    {
        _enableSpawning = true;
        maxEnemyLimit = GameManager.instance.maximumEnemyLimit;
        //InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
    }

    void ResetData()
    {
        _enableSpawning = false;
        currentEnemyCount = 0;
    }

    /// <summary>
    /// Method to spawn an Enemy GameObject.
    /// There is a delay to spawn each Enemy at a time interval.
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < maxEnemyLimit; ++i)
        {
            yield return new WaitForSeconds(spawnTime);

            if (_enableSpawning)
            {
                if (spawnPoints.Length <= 0)
                { // If no spawn points were given, then do nothing
                    Debug.LogError("[EnemySpawn]: Please assign spawn points.");
                }
                else
                {
                    // Create an enemy at a random spawn point
                    int spawnPointIndex = Random.Range(0, spawnPoints.Length);

                    // Create an instance of the enemy and spawn at the points
                    GameObject enemyObject = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                    ++currentEnemyCount;

                    NetworkServer.Spawn(enemyObject);
                    enemyObject.GetComponent<EnemyController>().enemyID = currentEnemyCount;
                }
            }
        }
    }
}
