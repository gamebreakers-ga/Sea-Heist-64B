using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardDetectionScript : MonoBehaviour
{
    private bool InView;
    private GuardScript guardScript;
    // Start is called before the first frame update
    void Start()
    {
        guardScript = GetComponentInParent<GuardScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InView)
        {
            guardScript.DetectionLevel += 0.2f;
        }

        if (guardScript.DetectionLevel >= 100)
        {
            guardScript.Detected = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InView = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InView = false;
        }
    }
}
