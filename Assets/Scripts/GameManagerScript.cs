using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static Player Player;
    public static UnityEngine.Object Bullet;
    void Awake()
    {
        Bullet = Resources.Load("PlayerBullet");
        //Player = GameObject.Find("Player").GetComponent<Player>();
        //if (Player == null)
        //{
        //    throw new NullReferenceException("Player is null");
        //}
        if (Bullet == null)
        {
            throw new NullReferenceException("Bullet is null");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {

    }
}