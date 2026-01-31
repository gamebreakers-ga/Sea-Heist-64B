using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject guardPrefab;
    public GameObject[] nodes;

    public GameObject swatPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnGuard(Vector3 Location, Quaternion Rotatation, bool Alerted)
    {
        GameObject result = Instantiate(guardPrefab, Location, Rotatation);
        result.GetComponent<GuardScript>().Detected = Alerted;
        return result;
    }
}
