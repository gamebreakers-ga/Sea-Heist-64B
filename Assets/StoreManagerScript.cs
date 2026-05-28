using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManagerScript : MonoBehaviour
{
    public GameManagerScript GM;

    public Dictionary<Paint, float> Guns; 
    private void Awake()
    {
        GM = GameObject.Find("Terrain").GetComponent<GameManagerScript>(); 
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(GM.Player.transform.position, transform.position) < 5)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Player.Paints.TryAdd(new PhonePistol<IHostile>(GM.Player, GM.Bullet));
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                Player.Paints.TryAdd(new AR<IHostile>(GM.Player, GM.Bullet));
            }
        }
    }
}