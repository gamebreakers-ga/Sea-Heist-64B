using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public float timer;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            explosion.GetComponent<Transform>().position = GetComponent<Transform>().position;
            Instantiate(explosion);
            Destroy(gameObject);
        }
    }
}
