using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public bool Detected;
    private GameObject Player;
    public float MovementSpeed;
    public float RotationSpeed;

    public GameObject[] PatrolRoute;
    public int NodeInPatrol;
    public PatrolingStatus PatrolingState = PatrolingStatus.Enroute;
    public PatrolingStatus LastPatrolState = PatrolingStatus.Enroute;
    public GuardStatus Status = GuardStatus.Calm;
    public float TimeAtSentry;
    public NavMeshAgent nma;
    float fireLimit = 0;

    public float DetectionLevel;
    public float LastAlert = 6;
    public Vector3 AlertPosition; 
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        NodeInPatrol = Random.Range(0, PatrolRoute.Length);
        Player.GetComponent<FirstPersonControllerScript>().GuardScripts.Add(this);
        nma = GetComponent<NavMeshAgent>();
        nma.destination = PatrolRoute[NodeInPatrol].transform.position;
        PatrolingState = PatrolingStatus.Enroute;
    }

    // Update is called once per frame
    void Update()
    {
        LastAlert += Time.deltaTime;
        if (LastAlert > 5 && DetectionLevel > 0 && !Detected)
        {
            DetectionLevel -= 0.1f;
        }

        if (DetectionLevel >= 33f)
        {
            PatrolingState = PatrolingStatus.Interrupted;
            
            if (DetectionLevel > 66f)
            {
                Status = GuardStatus.Investigative;
            }
        }

        if (DetectionLevel >= 100)
        {
            Detected = true;
        }

        if (PatrolingState == PatrolingStatus.Enroute && Vector3.Distance(nma.destination, transform.position) <= 2)
        {
            nma.isStopped = true;
            PatrolingState = PatrolingStatus.Waiting;
            TimeAtSentry = 0;
        }

        if (PatrolingState == PatrolingStatus.Waiting)
        {
            TimeAtSentry += Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, PatrolRoute[NodeInPatrol].transform.rotation, nma.angularSpeed * 0.2f);
            if (TimeAtSentry > 15)
            {
                nma.isStopped = false;
                PatrolingState = PatrolingStatus.Enroute;
                NextNode();
            }
        }

        if (PatrolingState == PatrolingStatus.Interrupted)
        {
            if (Status == GuardStatus.Calm)
            {
                transform.LookAt(AlertPosition);
                nma.isStopped = true;
            } else if (Status == GuardStatus.Investigative)
            {
                nma.Move(AlertPosition);
            }
        }

        if (Detected)
        {
            fireLimit += Time.deltaTime;
            if (Vector3.Distance(Player.transform.position, transform.position) < 10)
            {
                Quaternion rot = Quaternion.FromToRotation(Player.transform.position, gameObject.transform.position);
                nma.isStopped = true;
                if (rot.x < 10 && rot.y < 10 && rot.z < 10 && fireLimit > 5)
                {
                    fireLimit = 0;
                    Instantiate(bullet, transform.position + (gameObject.transform.forward * 2), transform.rotation);
                }
                transform.LookAt(Player.transform);
            } else
            {
                nma.destination = Player.transform.position;
                nma.isStopped = false;
            }
        }
    }

    public GameObject NextNode()
    {
        NodeInPatrol++;
        if (NodeInPatrol >= PatrolRoute.Length)
        {
            NodeInPatrol = 0;
        }
        nma.destination = PatrolRoute[NodeInPatrol].transform.position;
        return PatrolRoute[NodeInPatrol];
    }

    public void Lure(Vector3 Position)
    {
        DetectionLevel += 33f;
        LastAlert = 0;
    }
    public enum GuardStatus 
    {
        Calm,
        Investigative,
        Alert
    }

    public enum PatrolingStatus
    {
        Waiting,
        Enroute,
        Interrupted
    }
}
