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

    public GameObject bullet;
    public float firerate;
    public int ammo;
    public float reload;
    public float shoottime;
    public int maxammo;
    public Slider ammobar;
    public Slider reloading;
    public RawImage weaponred;
    public RawImage weaponblue;
    public bool multishot;
    public string weapon;
    public RawImage weapongreen;

    public float reloadmin;
    public float reloadmax;

    public GameObject playerbody;
    public Vector3 size;

    public GameObject bomb;
    public float bombrate;

    // Start is called before the first frame update
    void Start()
    {

        maxammo = 10;
        ammo = 10;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        reload = 100;
        shoottime = 1;
        bombrate = 10;
        reloading.gameObject.SetActive(false);
        weaponblue.gameObject.SetActive(false);
        weaponred.gameObject.SetActive(true);
        weapongreen.gameObject.SetActive(false);
        weapon = "red";
        reloadmin = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        reloadmax = reloadmin + 0.5f;
        if (Input.GetKeyDown("1"))
        {
            weaponred.gameObject.SetActive(true);
            weaponblue.gameObject.SetActive(false);
            weapongreen.gameObject.SetActive(false);
            maxammo = 10;
            ammo = 0;
            reload = 0;
            reloading.gameObject.SetActive(true);
            shoottime = 1;
            multishot = false;
            weapon = "red";
            reloadmin = 3f;
        }

         if (Input.GetKeyDown("2") && playerbody.GetComponent<player>().ownblue)
        {
            weaponblue.gameObject.SetActive(true);
            weaponred.gameObject.SetActive(false);
            weapongreen.gameObject.SetActive(false);
            maxammo = 2;
            ammo = 0;
            reload = 0;
            reloading.gameObject.SetActive(true);
            shoottime = 0.5f;
            multishot = true;
            weapon = "blue";
            reloadmin = 5;
        }
         if (Input.GetKeyDown("3") && playerbody.GetComponent<player>().owngreen)
        {
            weaponred.gameObject.SetActive(false);
            weaponblue.gameObject.SetActive(false);
            weapongreen.gameObject.SetActive(true);
            maxammo = 500;
            ammo = 0;
            reload = 0;
            reloading.gameObject.SetActive(true);
            shoottime = 0;
            multishot = false;
            weapon = "green";
            reloadmin = 10f;
        }
        reloading.GetComponent<Slider>().maxValue = reloadmin;

        reload += Time.deltaTime;
        firerate += Time.deltaTime;
        MouseLook();
        if (Input.GetKey(KeyCode.Mouse0) && ammo == 0 && reload > reloadmax)
        {
            reload = 0;
            ammo = 0;
            reloading.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            reloading.gameObject.SetActive(true);
            reload = 0;
            ammo = 0;
        }
        if (reload >= reloadmin && reload <= reloadmax)
        {
            reloading.gameObject.SetActive(false);
            ammo = maxammo;
        }
        reloading.GetComponent<Slider>().value = reload;
        if (Input.GetKey(KeyCode.Mouse1))
        {
            GetComponent<Camera>().fieldOfView = 15;
        }
        else
        {
            GetComponent<Camera>().fieldOfView = 60;
        }
        bombrate += Time.deltaTime;
        ammobar.GetComponent<Slider>().maxValue = maxammo;
        ammobar.GetComponent<Slider>().value = ammo;
    }
    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, minY, maxY);

        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        player.Rotate(Vector3.up * mouseX);

        if (Input.GetKey(KeyCode.Mouse0) && firerate >= shoottime && ammo > 0 && multishot == false)
        {
            bullet.GetComponent<Transform>().rotation = transform.rotation;
            bullet.GetComponent<Transform>().position = GetComponent<Transform>().position;
            firerate = 0;
            ammo--;
            Instantiate(bullet, transform.position + (gameObject.transform.forward), gameObject.transform.rotation);
        }
        if (Input.GetKey(KeyCode.Mouse0) && firerate >= shoottime && ammo > 0 && multishot == true)
        {
            bullet.GetComponent<Transform>().rotation = transform.rotation;
            bullet.GetComponent<Transform>().position = GetComponent<Transform>().position;
            firerate = 0;
            ammo--;

            for (int i = 0; i < 10; i++)
            {
                Instantiate(bullet, transform.position + (gameObject.transform.forward), gameObject.transform.rotation);
            }
        }
        if (Input.GetKey(KeyCode.G) && bombrate >= 10)
        {
            bomb.GetComponent<Transform>().rotation = transform.rotation;
            bomb.GetComponent<Transform>().position = GetComponent<Transform>().position;
            bombrate = 0;
            Instantiate(bomb, transform.position + (gameObject.transform.forward), gameObject.transform.rotation);
        }
    }
}
