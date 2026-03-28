using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] GuardNodes;
    public GameObject[] PoliceNodes;

    public GameObject GuardPrefab;
    public GameObject PolicePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public GuardScript SpawnSquad(int size)
    //{
    //    for (int i = 0; i < size; i++)
    //    {
    //        Instantiate(PolicePrefab, )
    //    }
    //}

    public GameObject GetRandom(GameObject[] GameObjects)
    {
        return GameObjects[Random.Range(0, GameObjects.Length)];
    }
}
