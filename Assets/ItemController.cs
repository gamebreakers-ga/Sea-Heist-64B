using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public bool isHeld;
    public Transform holderPosition;

    public GMscript gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GMscript>();
        holderPosition = GameObject.Find("PickUpHolder").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            transform.position = holderPosition.position;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            gm.targetsHit += 1;
            Destroy(collision.gameObject);
        }
    }


}
