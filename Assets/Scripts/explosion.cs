using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    public float timer;
    public Vector3 launch;
    public Vector3 grow;
    public GameObject particals;
    // Start is called before the first frame update
    void Start()
    {
        particals.GetComponent<Transform>().position = GetComponent<Transform>().position;
        Instantiate(particals);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        GetComponent<Transform>().localScale += grow;
        if (timer >= 1f)
        {
            Destroy(gameObject);
        }
        
    }
}
