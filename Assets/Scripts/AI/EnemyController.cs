using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyController : NetworkBehaviour
{
    // The maximum HP of the character, enemy, etc.
    [SyncVar]
    public int maxHitPoints;
    public int currentHitPoints { get; private set; }

    public float lookRadius = 10f;

    protected NavMeshAgent navMeshAgent;

    private Transform target;

    void OnEnable()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
    }

    void Update()
    {
        if (NetworkServer.active)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            if (players != null)
            {
                // Find the nearest target 
                target = GetNearestTarget(players);
            }
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius)
            {
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
    }

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

        return newTarget;
    }

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
}
