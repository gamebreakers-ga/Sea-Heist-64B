using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IEntity
{
    public float Health { get; }
    public void ApplyDamage(float Damage);
    public Transform cameraPosition { get; }
    public Transform transform { get; }
    public Vector3 cameraFoward { get; }

    Coroutine StartCoroutine(IEnumerator enumerator);
}

public interface IPlayer : IEntity
{

}

public interface IHostile : IEntity
{

}