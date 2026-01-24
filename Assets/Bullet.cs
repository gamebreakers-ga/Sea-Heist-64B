using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timer;
    public GameObject gm;
    public GameObject explosion;
    public GameObject healingPrefab;
    [Range(0f, 1f)]
    public float healSpawnChance = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        transform.Translate(Vector3.forward);

        if (timer >= 5)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            if (Random.value <= healSpawnChance)
            {
                Instantiate(
                    healingPrefab,
                    collision.transform.position,
                    Quaternion.identity
                );
            }
            Destroy(collision.gameObject);
            gm.GetComponent<GMscript>().score += 1;
            Destroy(gameObject);

        }
        if (collision.gameObject.tag == "notouch")
        {
            gm.GetComponent<GMscript>().score -= 1;
            Destroy(collision.gameObject);
            for (int i = 0; i < 2000; i++)
            {
                Instantiate(explosion, transform.position + (gameObject.transform.forward), gameObject.transform.rotation);
            }
        }
        if (collision.gameObject.tag == "time")
        {
            gm.GetComponent<GMscript>().timer += 2;
            Destroy(collision.gameObject);
        }
    }
}
