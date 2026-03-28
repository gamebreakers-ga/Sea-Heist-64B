using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour
{
    public GameObject[] roomHolder;
    public int flip = 1;
    public bool shouldflip = true;
    public GameObject[] pottargets;
    public int difflip;
    public int difficulty;
    public GameObject player;
    public int mintimes = 0;
    // Start is called before the first frame update
    void Start()
    {
        shouldflip = true;
        difficulty = player.GetComponent<player>().difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        if (flip == 1 && shouldflip)
        {
            Instantiate(roomHolder[0]);
            flip = Random.Range(1, 6);
        }
        else if (shouldflip && flip == 2)
        {
            Instantiate(roomHolder[2]);
            flip = Random.Range(1, 6);
        }
        else if (shouldflip && flip == 3)
        {
            Instantiate(roomHolder[3]);
            flip = Random.Range(1, 6);
        }
        else if (shouldflip && flip == 4)
        {
            Instantiate(roomHolder[4]);
            flip = Random.Range(1, 6);
        }
        //if adding more rooms, continue at this point from the number above and increase max flip by 1 per room.
        else if (shouldflip)
        {
            difflip = Random.Range(1, difficulty);
            mintimes += 1;
            if (difflip == 1 && mintimes >= difficulty)
            {
                Instantiate(roomHolder[1]);
            }
            pottargets = GameObject.FindGameObjectsWithTag("Doorway");
            if (pottargets.Length == 0)
            {
                shouldflip = false;
            }
        }
    }
}
