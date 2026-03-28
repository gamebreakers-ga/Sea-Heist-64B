using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorwayLinker : MonoBehaviour
{
    public GameObject[] target;
    public bool starter;
    public bool self;
    public int i = 0;
    public bool rancode = false;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rancode == false)
        {
            if (starter == false)
            {
                target = GameObject.FindGameObjectsWithTag("Doorway");
                if (!target[i].transform.IsChildOf(gameObject.transform) && target[i])
                {
                    GetComponent<Transform>().position = target[i].GetComponent<Transform>().position;
                    Destroy(target[i]);
                    rancode = true;
                }
                else
                {
                    i++;
                }
                if (i >= target.Length)
                {
                    Destroy(gameObject);
                }

            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (rancode)
        {
            if (collision.gameObject.tag != "floor" && collision.gameObject.tag != "Player")
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
