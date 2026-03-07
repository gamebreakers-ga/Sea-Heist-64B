using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playersetter : MonoBehaviour
{
    public GameObject player;
    public Vector3 spawn;
    public GameObject spawnpossetter;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FirstPersonController");
        spawn = spawnpossetter.GetComponent<Transform>().position;
        player.GetComponent<Transform>().position = spawn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
