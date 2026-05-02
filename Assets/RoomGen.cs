using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

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
    public int roomsspawned = 0;
    public GameObject navbaker;
    public bool firstmeshgen = true;
    // Start is called before the first frame update
    void Start()
    {
        shouldflip = true;
        difficulty = player.GetComponent<player>().difficulty;
        player = GameObject.Find("FirstPersonController");
    }

    // Update is called once per frame
    void Update()
    {
        if (roomsspawned >= 15 + difficulty)
        {
            flip = 5; //change this number to Range y - 1 if adding new rooms
        }
        if (flip == 1 && shouldflip)
        {
            Instantiate(roomHolder[0]);
            flip = Random.Range(1, 6);
            roomsspawned += 1;
        }
        else if (shouldflip && flip == 2)
        {
            Instantiate(roomHolder[2]);
            flip = Random.Range(1, 6);
            roomsspawned += 1;
        }
        else if (shouldflip && flip == 3)
        {
            Instantiate(roomHolder[3]);
            flip = Random.Range(1, 6);
            roomsspawned += 1;
        }
        else if (shouldflip && flip == 4)
        {
            Instantiate(roomHolder[4]);
            flip = Random.Range(1, 6);
            roomsspawned += 1;
        }
        //if adding more rooms, continue at this point from the number above and increase max flip by 1 per room.
        else if (shouldflip)
        {
            difflip = Random.Range(1, difficulty);
            mintimes += 1;
            if ((difflip == 1 && mintimes >= difficulty) || roomsspawned >= 50)
            {
                Instantiate(roomHolder[1]);
                roomsspawned += 1;
            }
            pottargets = GameObject.FindGameObjectsWithTag("Doorway");
            if (pottargets.Length <= 0)
            {
                shouldflip = false;
                if (navbaker.GetComponent<NavMeshSurface>().navMeshData == null)
                {
                    navbaker.GetComponent<TerrainBaker>().createmesh();
                }
            }
            flip = Random.Range(1, 6);
        }
    }
}
