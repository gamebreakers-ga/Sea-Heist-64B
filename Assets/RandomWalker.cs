using UnityEngine;
using UnityEngine.AI; // Required for NavMesh

public class RandomWalker : MonoBehaviour
{
    public float patrolRadius = 20f; // The agent will wander within this radius from its starting point
    public float minWaitTime = 1f; // Minimum time to wait before choosing a new destination
    public float maxWaitTime = 4f; // Maximum time to wait

    private NavMeshAgent agent;
    private Vector3 startPosition;
    private float waitTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        waitTime = Random.Range(minWaitTime, maxWaitTime); // Initial wait time
    }

    void Update()
    {
        // Check if the agent is close to its current destination
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // If so, start or continue waiting
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                // Time is up, find a new random destination
                Vector3 newPos = RandomNavmeshLocation(patrolRadius);
                agent.SetDestination(newPos);
                waitTime = Random.Range(minWaitTime, maxWaitTime); // Reset wait time
            }
        }
    }

    // Method to find a random point on the NavMesh within a given radius
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += startPosition;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

        // Check if the random point is on the NavMesh
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
