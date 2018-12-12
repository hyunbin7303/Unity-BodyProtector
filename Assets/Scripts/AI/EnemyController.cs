using Messyspace;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyController : NetworkBehaviour
{
    private const float kDefaultSpeed = 2.0f;

    public int enemyID = 0;

    [SyncVar]
    public float lookRadius = 10.0f;
    // The health of the enemy

    public Health health;

    [SyncVar]
    public bool playerInSight;

    // The speed of the enemy
    public float speed = kDefaultSpeed;
    // Where the player character is in relation to NPC
    public Vector3 direction;
    // How far away is the player from the NPC
    public float distance = 0.0f;

    protected NavMeshAgent navMeshAgent;
    private Transform target;

    void Awake()
    {
    }

    void Start()
    {
        // get reference to nav mesh agent
        this.navMeshAgent = GetComponent<NavMeshAgent>();
        // reference to the sphere collider
        health = GetComponent<Health>();
        // Has the player been sighted?
        playerInSight = false;
    }

    void Update()
    {
        if (!isServer)
            return;

        // Search for any players in the map
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players != null)
        {
            // Find the nearest target 
            target = GetNearestTarget(players);
        }
        else
        {
            target = null;
        }
    }

    void FixedUpdate()
    {
        if (!isServer)
            return;

        if (target != null)
        {
            // The enemy only follows the player if they are ALIVE
            //var targetStatus = target.GetComponent<PlayerStats>().status;
            //if (targetStatus == CharacterStatus.ALIVE)
            //{
            // Nearest target found, apply the distance calculations
            // on the server and have the enemy follow the nearest player
            //CmdUpdateNetwork();
            UpdateNetwork();
            //}
            //else
            //{
            //    CmdStopEnemy();
            //}
        }
        else
        {
            //CmdStopEnemy();
            StopEnemy();
        }
    }

    [Command]
    void CmdUpdateNetwork()
    {
        direction = target.position - transform.position;
        distance = Vector3.Distance(target.position, transform.position);

        if (navMeshAgent.speed <= 0.0f)
        {
            navMeshAgent.speed = speed;
        }
        if (distance <= lookRadius)
        {
            navMeshAgent.speed = speed;
            // Move towards the target (player)
            navMeshAgent.SetDestination(target.position);

            // If the enemy is near to the player...
            if (distance <= navMeshAgent.stoppingDistance)
            {
                // Attack the target

                // Face the target
                FaceTarget();
            }
        }
    }

    void UpdateNetwork()
    {
        direction = target.position - transform.position;
        distance = Vector3.Distance(target.position, transform.position);

        if (navMeshAgent.speed <= 0.0f)
        {
            navMeshAgent.speed = speed;
        }
        if (distance <= lookRadius)
        {
            navMeshAgent.speed = speed;
            // Move towards the target (player)
            navMeshAgent.SetDestination(target.position);

            // If the enemy is near to the player...
            if (distance <= navMeshAgent.stoppingDistance)
            {
                // Attack the target

                // Face the target
                FaceTarget();
            }
        }
    }

    /// <summary>
    /// Command to make the enemy stop.
    /// Changes the speed of the enemy to 0 which stops the enemy.
    /// </summary>
    [Command]
    void CmdStopEnemy()
    {
        navMeshAgent.speed = 0.0f;
    }

    void StopEnemy()
    {
        navMeshAgent.speed = 0.0f;
    }

    /// <summary>
    /// Make the enemy rotate and face their target.
    /// </summary>
    void FaceTarget()
    {
        // Get a direction vector of where the target is
        Vector3 direction = (target.position - transform.position).normalized;
        // Get a rotation of where the target is
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    Transform GetNearestTarget(GameObject[] targets)
    {
        if (targets.Length <= 0) { return null; }

        Transform newTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;   // Current position of the enemy

        foreach (GameObject potentialTarget in targets)
        {
            // The enemy only follows the player if they are ALIVE
            var targetStatus = potentialTarget.GetComponent<PlayerController>().status;
            if (targetStatus == CharacterStatus.ALIVE)
            {
                Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                float distSqrToTarget = directionToTarget.sqrMagnitude;
                // If the calculated distance is less than the closest target's distance
                // then we have found a new potential target
                if (distSqrToTarget < closestDistance)
                {
                    closestDistance = distSqrToTarget;
                    newTarget = potentialTarget.transform;  // Found the new target!
                }
            }
        }

        return newTarget;
    }


    #region Test
    /// <summary>
    /// Draw a wireframe sphere for the enemy's search radius.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    /// <summary>
    /// Draw a line towards the nearest target.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (target != null)
        {
            Vector3 direction = transform.position - target.position;
            float distance = direction.magnitude;

            if (distance > lookRadius)
                Gizmos.color = Color.red;

            Gizmos.DrawLine(transform.position, target.position);
        }
    }
    #endregion
}
