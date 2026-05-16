using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class TerrainBaker : MonoBehaviour
{
    public NavMeshSurface navmesh;
    public bool happened = false;
    // Start is called before the first frame update
    void Start()
    {
        navmesh = GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {
            
    }
    public void createmesh()
    {
        navmesh.BuildNavMesh();
    }
}
