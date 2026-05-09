using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGiver : MonoBehaviour
{
    public int numFish;
    public float fishingTimer;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        numFish = Random.Range(5, 21);
    }

    // Update is called once per frame
    void Update()
    {
        if (fishingTimer >= 2)
        {
            if (numFish > 0)
            {
                player.GetComponent<player>().fishHeld += 1;
                numFish -= 1;
            } 
            else
            {
                Debug.Log("Out Of Fish");
            }
            fishingTimer = 0;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            fishingTimer += Time.deltaTime;
        }
    }
}
