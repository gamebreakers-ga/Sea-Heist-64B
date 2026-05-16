using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class RoomGen : MonoBehaviour
{
    public List<DoorwayLinker> roomPrefabs = new();
    public DoorwayLinker closingRoomPrefab;
    public int flip = 1;
    public int difflip;
    public int difficulty;
    public GameObject player;
    public int mintimes = 0;
    public List<DoorwayLinker> roomsSpawned = new();
    public TerrainBaker navbaker;
    public bool firstmeshgen = true;

    public int minRooms = 10;
    public int maxRooms = 25;
    public DoorwayLinker startingRoom;
    public List<GameObject> possibleDoorways = new();

    // Start is called before the first frame update
    void Start()
    {
        difficulty = player.GetComponent<player>().difficulty;
        player = GameObject.Find("FirstPersonController");

        roomsSpawned.Add(startingRoom);
        possibleDoorways.AddRange(startingRoom.exits);
        navbaker.createmesh();

        Generate();
    }

    private void Generate()
    {
        int totalRooms = Random.Range(minRooms, maxRooms + 1);

        for(int i = 0; i < totalRooms; i++)
        {
            SpawnRandomRoom();
        }

        while(possibleDoorways.Count > 0)
        {
            SpawnRandomRoom(true);
        }

        StartCoroutine(BuildNavMesh());
        /*if (roomsSpawned.Count >= 15 + difficulty)
        {
            SpawnRandomRoom(true); // spawn closing room
        }
        if (flip == 1)
        {
            SpawnRandomRoom(0);
            
        }
        else if (flip == 2)
        {
            SpawnRandomRoom(2);
            flip = Random.Range(1, 6);
        }
        else if (flip == 3)
        {
            SpawnRandomRoom(3);
            flip = Random.Range(1, 6);
        }
        else if (flip == 4)
        {
            SpawnRandomRoom(4);
            flip = Random.Range(1, 6);
        }
        //if adding more rooms, continue at this point from the number above and increase max flip by 1 per room.
        else
        {
            difflip = Random.Range(1, difficulty);
            mintimes += 1;
            if ((difflip == 1 && mintimes >= difficulty) || roomsSpawned.Count >= 50)
            {
                SpawnRandomRoom(1);
            }
            flip = Random.Range(1, 6);
        }*/
    }

    private void SpawnRandomRoom(bool isClosing = false)
    {
        DoorwayLinker randomRoomPrefab = roomPrefabs[UnityEngine.Random.Range(0, roomPrefabs.Count)];
        if (isClosing)
            randomRoomPrefab = closingRoomPrefab;
        DoorwayLinker newSpawnedRoom = Instantiate(randomRoomPrefab);
        roomsSpawned.Add(newSpawnedRoom);
        possibleDoorways.AddRange(newSpawnedRoom.exits);
        newSpawnedRoom.Snap(this);
    }

    private IEnumerator BuildNavMesh()
    {
        yield return null; // wait 1 frame

        navbaker.createmesh();
    }
}
