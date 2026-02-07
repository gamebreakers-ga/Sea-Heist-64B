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
    public float throwPower;
    public float throwMultiplier;
    public Slider powerMeter;


    public bool itemHeld;
    public GameObject currentItem;
    public Transform cameraPosition;
    public LayerMask interactMask;
    public GameObject bullet;
    public GameObject looking;

    public GameObject bomb;
    public float bombrate;

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
        powerMeter.gameObject.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!itemHeld)
            {
                if (Physics.Raycast(cameraPosition.position, cameraPosition.forward, out RaycastHit reach, 3f, interactMask))
                {
                    itemHeld = true;
                    currentItem = reach.collider.gameObject;
                    currentItem.GetComponent<ItemController>().isHeld = true;
                    currentItem.GetComponent<Rigidbody>().useGravity = false;
                }
            }
            else
            {
                itemHeld = false;
                currentItem.GetComponent<ItemController>().isHeld = false;
                currentItem.GetComponent<Rigidbody>().useGravity = true;
                currentItem = null;
            }
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
        powerMeter.value = throwPower;

        if (Input.GetKeyDown(KeyCode.E) && itemHeld)
        {
            throwPower = 0;
            powerMeter.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.E) && itemHeld)
        {
            throwPower = Mathf.PingPong(Time.time, 1);
        }
        if (Input.GetKeyUp(KeyCode.E) && itemHeld)
        {
            itemHeld = false;
            currentItem.GetComponent<ItemController>().isHeld = false;
            currentItem.GetComponent<Rigidbody>().useGravity = true;
            currentItem.GetComponent<Rigidbody>().AddForce(throwMultiplier * throwPower * cameraPosition.forward, ForceMode.Impulse);
            currentItem = null;
            powerMeter.gameObject.SetActive(false);
            throwPower = 0;
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

        Collider doorCollider = door.GetComponent<BoxCollider>();

        door.Rotate(0f, 90f, 0f);
        doorCollider.enabled = false;

        yield return new WaitForSeconds(3f);

        door.Rotate(0f, -90f, 0f);
        doorCollider.enabled = true;

        open = false;
    }



}
