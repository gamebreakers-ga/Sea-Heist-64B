using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity;
    public float xRot = 0f;
    public float minY = -75f;
    public float maxY = 75f;


    public GameObject playerbody;
    public Vector3 size;

    public GameManagerScript GM;
    public player Player;
    // Start is called before the first frame update
    void Start()
    {
        //GM = GameObject.Find("Terrain").GetComponent<GameManagerScript>();
        Player = GetComponentInParent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseLook();
        if (Input.GetKey(KeyCode.Mouse1))
        {
            GetComponent<Camera>().fieldOfView = 15;
        }
        else
        {
            GetComponent<Camera>().fieldOfView = 60;
        }
    }
    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, minY, maxY);

        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        Player.transform.Rotate(Vector3.up * mouseX);
    }
}
