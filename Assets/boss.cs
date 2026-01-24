using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public int bosshp;
    public GameObject player;
    public GameObject gm;
    // Start is called before the first frame update
    void Start()
    {
        bosshp = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (bosshp <= 0)
        {
            gm.GetComponent<GMscript>().score += 3;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
            bosshp -= 1;
        }
    }
}
