using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detector : MonoBehaviour
{
    public GameObject GuardManager;

    public bool InView = false;
    // Start is called before the first frame update
    void Start()
    {
        GuardManager = GameObject.Find("GuardManager");
        StartCoroutine(EnableCollider());
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(2);
        GetComponent<MeshCollider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (InView)
        {
            GuardManager.GetComponent<GuardManager>().anger += 1f;
        } 
        else
        {
            GuardManager.GetComponent<GuardManager>().anger -= 0.1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InView = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InView = false;
        }
    }
}
