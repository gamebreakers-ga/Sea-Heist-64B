using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IHostile
{
    float Health { get; }
    void ApplyDamage(float Damage);
}