using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using Unity;

public abstract class Paint
{
    public abstract IEntity Holder { get; protected set; }
    public abstract string Name { get; }
    public abstract int Ammo { get; protected set; }
    public abstract int MagizineSize { get; }
    public abstract bool IsReloading { get; protected set; }
    public abstract float Range { get;  }
    public abstract float Damage { get; }
    public abstract UnityEngine.Object Bullet { get; protected set; }
    public abstract void Shoot();
    public abstract Task Reload();
}

public abstract class Paint<T> : Paint where T : IEntity
{
    protected void FireRay(Ray ray)
    {
        Ammo--;
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Range, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            ((GameObject)GameObject.Instantiate(Bullet, ray.origin, Quaternion.identity)).GetComponent<BulletScript>().SetEndPoint(ray.direction * Range, Range);
            if (hitInfo.collider.gameObject.TryGetComponent(out T script))
            {
                script.ApplyDamage(Damage);
            }
        }
        else
        {
            ((GameObject)GameObject.Instantiate(Bullet, ray.origin, Quaternion.identity)).GetComponent<BulletScript>().SetEndPoint(ray.direction * Range, Range);
        }
    }
}

public class Barrett<T> : Paint<T> where T : IEntity
{
    public override IEntity Holder { get; protected set; }
    public override string Name => "Barrett M82";
    public override float Range => 80;
    public override int MagizineSize => 5;
    public override float Damage => 80;
    public override int Ammo { get; protected set; }
    public override bool IsReloading { get; protected set; }
    public TimeSpan FireRate => TimeSpan.FromSeconds(1);
    public Stopwatch FireRateTimer { get; } = new();
    public TimeSpan ReloadTime { get; } = TimeSpan.FromSeconds(0.5);
    public override UnityEngine.Object Bullet { get; protected set; }


    public Barrett(IEntity player, UnityEngine.Object bullet)
    {
        Bullet = bullet;
        Holder = player;
        FireRateTimer.Start();
        Ammo = MagizineSize;
    }

    public override async void Shoot()
    {
        if (Ammo == 0)
        {
            await Reload();
            return;
        } else if (FireRateTimer.Elapsed < FireRate)
        {
            return;
        } else
        {
            FireRay(new Ray(Holder.cameraPosition.position, Holder.cameraFoward));
        }
        FireRateTimer.Restart();
    }

    public override async Task Reload()
    {
        if (IsReloading) return; 
        await Task.Delay(ReloadTime);
        Ammo = (Ammo > 0) ? MagizineSize + 1 : MagizineSize;
    }
}

public class AR<T> : Paint<T> where T : IEntity
{
    public override IEntity Holder { get; protected set; }
    public override string Name => "AR-57";
    public override float Range => 30;
    public override int MagizineSize => 25;
    public override float Damage => 26;
    public override int Ammo { get; protected set; }
    public override bool IsReloading { get; protected set; }
    public TimeSpan FireRate => TimeSpan.FromSeconds(0.5);
    public Stopwatch FireRateTimer { get; } = new();
    public TimeSpan ReloadTime { get; } = TimeSpan.FromSeconds(0.5);

    public override UnityEngine.Object Bullet { get; protected set; }


    public AR(IEntity player, UnityEngine.Object bullet)
    {
        Bullet = bullet;
        Holder = player;
        FireRateTimer.Start();
        Ammo = MagizineSize;
    }

    public override async void Shoot()
    {
        if (Ammo == 0)
        {
            await Reload();
            return;
        } else if (FireRateTimer.Elapsed < FireRate)
        {
            return;
        } else
        {
            FireRay(new Ray(Holder.cameraPosition.position, Holder.cameraFoward));
        }
        FireRateTimer.Restart();
    }

    public override async Task Reload()
    {
        if (IsReloading) return;
        await Task.Delay(ReloadTime);
        Ammo = (Ammo > 0) ? MagizineSize + 1 : MagizineSize;
    }
}

public class PhonePistol<T> : Paint<T> where T : IEntity
{
    public override IEntity Holder { get; protected set; }
    public override string Name => "Ideal Conceal";
    public override float Range => 10;
    public override int MagizineSize => 3;
    public override float Damage => 17;
    public override int Ammo { get; protected set; }
    public override bool IsReloading { get; protected set; }
    public TimeSpan FireRate => TimeSpan.FromSeconds(1);
    public Stopwatch FireRateTimer { get; } = new();
    public TimeSpan ReloadTime { get; } = TimeSpan.FromSeconds(0.5);
    public override UnityEngine.Object Bullet { get; protected set; }

    public PhonePistol(IEntity player, UnityEngine.Object bullet)
    {
        Bullet = bullet;
        Holder = player;
        FireRateTimer.Start();
        Ammo = MagizineSize;
    }

    public override async void Shoot()
    {
        if (Ammo == 0)
        {
            await Reload();
            return;
        } else if (FireRateTimer.Elapsed < FireRate)
        {
            return;
        } else
        {
            FireRay(new Ray(Holder.cameraPosition.position, Holder.cameraFoward));
        }
        FireRateTimer.Restart();
    }

    public override async Task Reload()
    {
        if (IsReloading) return;
        await Task.Delay(ReloadTime);
        Ammo =  MagizineSize;
    }
}

public class FN<T> : Paint<T> where T : IEntity
{
    public override IEntity Holder { get; protected set; }
    public override string Name => "FN 510";
    public override int MagizineSize => 28;
    public override int Ammo { get; protected set; }
    public override bool IsReloading { get; protected set; }

    public override float Range => 1000;
    public override float Damage => 4;
    public readonly TimeSpan FireRate = TimeSpan.FromSeconds(0.5);
    public readonly Stopwatch FireRateTimer = new Stopwatch();
    public readonly TimeSpan ReloadTime = TimeSpan.FromSeconds(1);
    public override UnityEngine.Object Bullet { get; protected set; }

    public FN(IEntity player, UnityEngine.Object bullet)
    {
        Bullet = bullet;
        Holder = player;
        FireRateTimer.Start();
        Ammo = MagizineSize;
    }

    public override async void Shoot()
    {
        if (IsReloading) return;
        if (Ammo == 0)
        {
            await Reload();
            return;
        } else if (FireRateTimer.Elapsed < FireRate)
        {
            return;
        } else
        {
            FireRay(new Ray(Holder.cameraPosition.position, Holder.cameraFoward));
        }
        FireRateTimer.Restart();
    }

    public override async Task Reload()
    {
        if (IsReloading) return;
        await Task.Delay(ReloadTime);
        Ammo = (Ammo > 0) ? MagizineSize + 1 : MagizineSize;
    }
}