using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    /// The camera
    public Camera cam;

    // The NavMeshAgent is how the object will navigate through out scene
    // The NavMesh must be baked first, so the agent can recognize the available paths.
    NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Move the object based on the mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Move the nav agent
                agent.SetDestination(hit.point);
            }
        }
    }
}
