using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // The maximum HP of the character, enemy, etc.
    public int maxHitPoints;
    public int currentHitPoints { get; private set; }

    public float lookRadius = 10f;

    protected NavMeshAgent navMeshAgent;

    private Transform target;

    void OnEnable()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            navMeshAgent.SetDestination(target.position);

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

    /// <summary>
    /// Draw a wireframe sphere for the enemy's search radius.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
