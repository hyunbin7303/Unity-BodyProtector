using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyController : NetworkBehaviour
{
    [SyncVar]
    public float lookRadius = 10.0f;
    // The health of the enemy

    public Health health;

    [SyncVar]
    public bool playerInSight;

    // Where the player character is in relation to NPC
    public Vector3 direction;
    // How far away is the player from the NPC
    public float distance = 0.0f;
    // What is the angle between the PC and NPC
    public float angle = 0.0f;
    // what is the field of view for our NPC?
    public float fieldOfViewAngle = 360.0f;
    // calculate the angle between PC and NPC
    public float calculatedAngle;

    protected NavMeshAgent navMeshAgent;
    private SphereCollider col;
    private Transform target;

    void Awake()
    {
        // get reference to nav mesh agent
        this.navMeshAgent = GetComponent<NavMeshAgent>();
        // reference to the sphere collider
        this.col = GetComponent<SphereCollider>() as SphereCollider;
        health = GetComponent<Health>();
        // Has the player been sighted?
        playerInSight = false;
    }

    void Start()
    {
    }

    void Update()
    {
        if (!isServer)
            return;

        if (NetworkServer.active)
        {
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

            if(health.currentHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        if (!isServer)
            return;

        if (target != null)
        {
            // Nearest target found, apply the distance calculations
            // on the server and 
            CmdUpdateNetwork();
        }
    }

    [Command]
    void CmdUpdateNetwork()
    {
        direction = target.position - transform.position;
        distance = Vector3.Distance(target.position, transform.position);

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
