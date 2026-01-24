using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FirstPersonControllerScript : MonoBehaviour
{
    public Vector3 moveDirection;
    public float speed;
    public float jump;

    public LayerMask groundMask;

    public Transform cameraPosition;
    public LayerMask interactMask;
    public LayerMask EnemyLayer;

    public Rigidbody rigidBody;

    public Slider DetectionSlider;

    public List<GuardScript> GuardScripts;

    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = GameObject.Find("Main Camera").transform;
        rigidBody = GetComponent<Rigidbody>();

        DetectionSlider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        SetDetection();
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(x, 0, z);
        transform.Translate(speed * Time.deltaTime * moveDirection);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rigidBody.AddForce(jump * Vector3.up, ForceMode.Impulse); // Impulse makes AddForce take the mass of the rb into account.
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit[] Targets = Physics.SphereCastAll(transform.position, 20f, transform.forward, 7);

            foreach (RaycastHit hit in Targets)
            {
                GuardScript GS = hit.collider.gameObject.GetComponent<GuardScript>();
                if (GS != null && GS.Status != GuardScript.GuardStatus.Alert)
                {
                    GS.Lure(transform.position);
                    Debug.Log($"Guard Lured: {GS.DetectionLevel}");
                    break;
                }
            }
            
        }
    }

    bool IsGrounded()
    {
        if (Physics.Raycast(transform.position - new Vector3(0, 0.9f, 0), Vector3.down, out RaycastHit hit, 0.2f, groundMask)) 
        {
            return hit.collider.gameObject.CompareTag("Ground");
        }
        return false;    
    }

    public void SetDetection()
    {
        float LargestDetection = 0;
        foreach (GuardScript guardScript in GuardScripts)
        {
            if (guardScript.DetectionLevel > LargestDetection)
            {
                LargestDetection = guardScript.DetectionLevel;
            }
        }
        DetectionSlider.value = LargestDetection;
        DetectionSlider.enabled = (LargestDetection != 0);
    }
}
