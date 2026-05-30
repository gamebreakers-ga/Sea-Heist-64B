using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player playerh = collision.gameObject.GetComponent<player>();

            if (playerh != null && playerh.Health < 90)
            {
                playerh.Health += 10;
            }
            if(playerh.Health >= 90){
                playerh.Health = 100;
            }

            Destroy(gameObject);
        }
    }
}
