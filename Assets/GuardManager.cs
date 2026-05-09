using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardManager : MonoBehaviour
{
    public float anger = 0;
    public bool detected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anger > 10)
        {
            detected = true;
        }
        if (anger < 0)
        {
            anger = 0;
        }
    }
}
