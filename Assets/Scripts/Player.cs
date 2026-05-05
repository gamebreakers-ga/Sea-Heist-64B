using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour, IPlayer
{
    public static float Money = 0;


    public bool open;
    public Vector3 moveDirection;
    public float speed;

    public LayerMask groundMask;
    public float jumpForce;
    public Rigidbody rb;

    public GameObject looking;

    public float stamina;
    public float staregen;
    public Slider sprintbar;

    public bool broken;
    public Vector3 reset;

    private float newH;

    private bool crouch;

    public Quaternion standardrotate;

    public Transform camPiv;
    public Transform cameraPosition => camPiv;
    public Vector3 cameraFoward => cameraPosition.forward;

    public bool key;

    public Selection<IPaint<IHostile>> Paints;
    public float Health { get; set; } = 100;

    public TextMeshProUGUI AmmoDisplay;

    // Start is called before the first frame update
    void Start()
    {
        Paints = new Selection<IPaint<IHostile>>(3) { new FN<IHostile>(this) };
        Paints.TrySelect(0);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Paints.Selected.IsReloading)
        {
            AmmoDisplay.text = "Reloading";
        } else
        {
            AmmoDisplay.text = $"{Paints.Selected.Ammo}/{Paints.Selected.MagizineSize}";
        }

        for (int i = 0; i < Paints.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                Paints.TrySelect(i);
                break;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Paints.Selected.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Paints.Selected.Reload();
        }
        
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

        if (!crouch && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0, jumpForce, 0);
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

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
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
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position - new Vector3(0, .9f, 0), Vector3.down, out RaycastHit hit, .2f, groundMask);
    }

    public void ApplyDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Debug.Log("DEAD");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "key"){
            key = true;
        }
        if (collision.gameObject.tag == "door" && key && !open)
        {
            StartCoroutine(DoorTimer(collision.transform));
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