using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IHostile
{
    public float Health { get; }
    public void ApplyDamage(float Damage);
    public Transform cameraPosition { get; }
    public LayerMask EnemyLayer { get; }
    public Transform transform { get; }
    public Vector3 cameraFoward { get; }
}