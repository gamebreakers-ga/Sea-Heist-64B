using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardScript2 : MonoBehaviour
{
    NavMeshAgent agent;
    private RaycastHit objectHit;
    private Transform raycastObject;
    public GameObject player;
    public float aggrotimer;
    public bool detected = false;
    public Vector3 playerpos;
    public float timer;
    public GameObject[] patrolroute;
    public int i = 0;
    public Vector3 targetnode;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerpos = player.GetComponent<Transform>().position;
        targetnode = patrolroute[i].GetComponent<Transform>().position;
        Vector3 fwd = raycastObject.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(raycastObject.transform.position, fwd * 50, Color.green);
        if (Physics.Raycast(raycastObject.transform.position, fwd, out objectHit, 50))
        {
            if (objectHit.collider.gameObject.CompareTag("player"))
            {
                aggrotimer += 0.1f;
                if (aggrotimer >= 100)
                {
                    detected = true;
                }
            } else
            {
                if (aggrotimer > 0)
                {
                    aggrotimer -= 0.05f;
                }
            }
        } else
        {
            if (aggrotimer > 0)
            {
                aggrotimer -= 0.05f;
            }
        }
        if (detected == true) 
        {
            agent.SetDestination(playerpos);
        } else if (agent.isStopped)
        {
            agent.SetDestination(targetnode);
            i++;
            if (i > patrolroute.Length - 1)
            {
                i = 0;
            }
        }
    }
}
