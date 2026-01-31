using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class player : MonoBehaviour
{
    public int health;
    public bool open;
    public Vector3 moveDirection;
    public float speed;

    public LayerMask groundMask;
    public float jumpForce;
    public Rigidbody rb;

    public Transform cameraPosition;
    public LayerMask interactMask;
    public GameObject bullet;
    public GameObject looking;


    public float stamina;
    public float staregen;
    public Slider sprintbar;

    public bool broken;
    public Vector3 reset;

    private float newH;

    private bool crouch;

    public bool ko;
    public float getuptimer;
    public Quaternion standardrotate;

    public Transform camPiv;

    public bool atstore = false;
    public bool inui = false;
    public GameObject storetext;
    public bool owngreen = false;
    public bool ownblue = false;

    public GameObject redicon;
    public GameObject blueicon;
    public GameObject greenicon;

    public bool key;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        open = false;
        key=false;
        ko = false;
        crouch = false;
        newH = 0.0000000000f;
        stamina = 10;
        staregen = 3;
        rb = GetComponent<Rigidbody>();
        storetext.gameObject.SetActive(false);
        redicon.gameObject.SetActive(true);
        blueicon.gameObject.SetActive(false);
        greenicon.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
        
        if (Input.GetKey(KeyCode.LeftControl))
        {
            crouch = true;
            speed = 0.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouch = false;
            speed = 5;
        }

        if (ownblue)
        {
            blueicon.gameObject.SetActive(true);
        }
        if (owngreen)
        {
            greenicon.gameObject.SetActive(true);
        }

        float TY = crouch ? newH : 1;
        Vector3 targetPos = new Vector3(camPiv.localPosition.x, TY, camPiv.localPosition.z);
        camPiv.localPosition = Vector3.Lerp(camPiv.localPosition, targetPos, Time.deltaTime * 8f);

        if (GetComponent<Transform>().position.y <= -100)
        {
            GetComponent<Transform>().position = reset;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (broken)
            {
                broken = false;
            }
            else
            {
                broken = true;
            }
        }
        if (broken)
        {
            gameObject.GetComponent<Transform>().rotation = looking.GetComponent<Transform>().rotation;
        }
        if (Input.GetKeyDown(KeyCode.F) && atstore)
        {
            inui = true;
            Cursor.lockState = CursorLockMode.None;
            storetext.gameObject.SetActive(true);
        }
        else if(atstore == false)
        {
            inui = false;
            Cursor.lockState = CursorLockMode.Locked;
            storetext.gameObject.SetActive(false);
        }
        if (atstore)
        {
            //buythings
            if (Input.GetKeyDown(KeyCode.U))
            {
                ownblue = true;
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                owngreen = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GetComponent<Rigidbody>().freezeRotation = false;
            GetComponent<Rigidbody>().useGravity = false;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetComponent<Rigidbody>().freezeRotation = true;
            GetComponent<Rigidbody>().useGravity = true;
        }

        moveDirection = new Vector3(x, 0, z);
        transform.Translate(speed * Time.deltaTime * moveDirection);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && crouch == false)
        {
            speed = 9;
            stamina -= Time.deltaTime;
            staregen = 0;
            
        }
        else
        {
            speed = 5;
            staregen += Time.deltaTime;
            if (staregen >= 3)
            {
                stamina += Time.deltaTime;
            }
        }
        sprintbar.GetComponent<Slider>().value = stamina;
        if (stamina > 10)
        {
            stamina = 10;
        }
        if (staregen > 3)
        {
            staregen = 3;
        }
        if (staregen < 0)
        {
            staregen = 0;
        }

       if (ko)
        {
            getuptimer += Time.deltaTime;
            speed = 0;
        }
       if (getuptimer >= 5 & getuptimer <= 5.5)
        {
            GetComponent<Rigidbody>().freezeRotation = true;
            GetComponent<Transform>().rotation = standardrotate;
            speed = 5;
        }
       if (getuptimer >= 6)
        {
            ko = false;
        }
       if (Input.GetKeyDown(KeyCode.T))
        {
            ko = true;
            GetComponent<Rigidbody>().freezeRotation = false;
            getuptimer = 0;
        }
       
    }

    bool IsGrounded()
    {
        if (Physics.Raycast(transform.position - new Vector3(0, .9f, 0), Vector3.down,
            out RaycastHit hit, .2f, groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bomb")
        {
            GetComponent<Rigidbody>().freezeRotation = false;
            ko = true;
            getuptimer = 0;
        }
        if (collision.gameObject.tag == "key"){
            key = true;
        }
        if (collision.gameObject.tag == "store")
        {
            atstore = true;
        }
        if (collision.gameObject.tag == "door" && key && !open)
        {
            StartCoroutine(DoorTimer(collision.transform));
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "store")
        {
            atstore = false;
        }
    }
    IEnumerator DoorTimer(Transform door)
    {
        open = true;

        Transform pivot = door.parent;
        Collider doorCollider = door.GetComponent<BoxCollider>();

        float openDuration = 1.5f;
        float elapsed = 0f;

        Quaternion startRot = pivot.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0f, -90f, 0f);

        doorCollider.enabled = false;

        while (elapsed < openDuration)
        {
            elapsed += Time.deltaTime;
            pivot.rotation = Quaternion.Lerp(startRot, endRot, elapsed / openDuration);
            yield return null;
        }

        pivot.rotation = endRot;

        yield return new WaitForSeconds(3f);

        elapsed = 0f;
        while (elapsed < openDuration)
        {
            elapsed += Time.deltaTime;
            pivot.rotation = Quaternion.Lerp(endRot, startRot, elapsed / openDuration);
            yield return null;
        }

        pivot.rotation = startRot;

        doorCollider.enabled = true;
        open = false;
    }



}
