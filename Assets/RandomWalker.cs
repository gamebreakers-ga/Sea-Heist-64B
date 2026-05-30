using UnityEngine;
using UnityEngine.AI; // Required for NavMesh
using System.Collections;
public class RandomWalker : MonoBehaviour, IHostile
{
    public float patrolRadius = 20f; // The agent will wander within this radius from its starting point
    public float minWaitTime = 1f; // Minimum time to wait before choosing a new destination
    public float maxWaitTime = 4f; // Maximum time to wait

    private NavMeshAgent agent;
    private Vector3 startPosition;
    private float waitTime;

    public GameObject GuardManager;

    public GameObject player;

    public Vector3 playerpos;

    public float shoottimer = 0;

    public GameObject bullet;

    public float health = 5;
    public float Health => health;

    public FN<IPlayer> Gun;

    public Transform cameraPosition { get; protected set; }
    public Vector3 cameraFoward => -cameraPosition.forward;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        waitTime = Random.Range(minWaitTime, maxWaitTime); // Initial wait time
        GuardManager = GameObject.Find("GuardManager");
        player = GameObject.FindGameObjectWithTag("Player");

        cameraPosition = GetComponentInChildren<detector>().gameObject.transform;
        //Gun = new FN<IPlayer>(this, GameManagerScript.l);
        StartCoroutine(RemoveIfStuck());
    }

    void Update()
    {
        playerpos = player.GetComponent<Transform>().position;
        if (GuardManager.GetComponent<GuardManager>().detected == false)
        {
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
        else
        {
            Gun.Shoot();
        }
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator RemoveIfStuck()
    {
        yield return new WaitForSeconds(10);
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity, 6)) {
            transform.position = hitInfo.transform.position;
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

    public void ApplyDamage(float Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
