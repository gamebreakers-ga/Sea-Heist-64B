using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Bullet : MonoBehaviour
{
    public float Damage;

    private void OnCollisionEnter(Collision collision)
    {

        switch (collision.gameObject.tag)
        {
            case "Guard":
                collision.gameObject.GetComponent<GuardScript>().ApplyDamage(Damage);
                break;
        }
    }
}
