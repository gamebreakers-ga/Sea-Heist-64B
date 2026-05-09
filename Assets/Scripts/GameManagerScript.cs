using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public Player Player;
    public UnityEngine.Object Bullet;
    private void Awake()
    {
        Bullet = Resources.Load("PlayerBullet");
        Player = GameObject.Find("Player").GetComponent<Player>();
        if (Player == null)
        {
            throw new Exception("Player is null");
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

public enum Scene
{
    Menu,
    Tutorial
}