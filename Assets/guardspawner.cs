using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardspawner : MonoBehaviour
{
    public int roll;
    public GameObject guard;
    // Start is called before the first frame update
    void Start()
    {
        roll = Random.Range(1, 51);
        if (roll == 1)
        {
            guard.GetComponent<Transform>().position = GetComponent<Transform>().position;
            Instantiate(guard);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
