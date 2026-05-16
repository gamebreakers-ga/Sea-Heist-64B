using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
public class guardspawner : MonoBehaviour
{
    public int roll;
    public GameObject guard;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        spawnguard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public async void spawnguard()
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        roll = UnityEngine.Random.Range(1, 16);
        if (roll == 1)
        {
            guard.GetComponent<Transform>().position = GetComponent<Transform>().position;
            Instantiate(guard);
        }
    }
}
