using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.WSA;

public abstract class Paint
{
    public abstract IEntity Holder { get; protected set; }
    public abstract string Name { get; }
    public abstract int Ammo { get; protected set; }
    public abstract int MagizineSize { get; }
    public abstract bool IsReloading { get; protected set; }
    public abstract void Shoot();
    public abstract Task Reload();

    protected async void DrawLine(Vector3 endPos)
    {
        Holder.LineRenderer.enabled = true;

        Holder.LineRenderer.SetPosition(0, Holder.transform.position);
        Holder.LineRenderer.SetPosition(1, endPos);

        await Task.Delay(TimeSpan.FromSeconds(0.5));

        Holder.LineRenderer.enabled = false;
    }
}

public abstract class Paint<T> : Paint where T : IEntity
{
    
}

public class Barrett<T> : Paint<T> where T : IEntity
{
    public override IEntity Holder { get; protected set; }
    public override string Name => "Barrett M82";
    public float Range => 80;
    public override int MagizineSize => 5;
    public float Damage => 80;
    public override int Ammo { get; protected set; }
    public override bool IsReloading { get; protected set; }
    public TimeSpan FireRate => TimeSpan.FromSeconds(1);
    public Stopwatch FireRateTimer { get; } = new();
    public TimeSpan ReloadTime { get; } = TimeSpan.FromSeconds(0.5);

    public Barrett(IEntity player)
    {
        Holder = player;
        FireRateTimer.Start();

        Ammo = MagizineSize;
    }

    public override async void Shoot()
    {
        if (Ammo == 0)
        {
            UnityEngine.Debug.Log(" no ammo reloading");
            await Reload();
        } else if (FireRateTimer.Elapsed < FireRate)
        {
            return;
        } else
        {
            Ammo--;
            if (Physics.Raycast(Holder.cameraPosition.position, Holder.cameraFoward, out RaycastHit hitInfo, Range, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                DrawLine(hitInfo.collider.gameObject.transform.position);
                if (hitInfo.collider.gameObject.TryGetComponent(out T script))
                {
                    script.ApplyDamage(Damage);
                }
            }
            else
            {
                DrawLine(Holder.cameraFoward * Range);
            }
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
    public float Range => 30;
    public override int MagizineSize => 25;
    public float Damage => 26;
    public override int Ammo { get; protected set; }
    public override bool IsReloading { get; protected set; }
    public TimeSpan FireRate => TimeSpan.FromSeconds(0.5);
    public Stopwatch FireRateTimer { get; } = new();
    public TimeSpan ReloadTime { get; } = TimeSpan.FromSeconds(0.5);

    public AR(IEntity player)
    {
        Holder = player;
        FireRateTimer.Start();
        Ammo = MagizineSize;
    }

    public override async void Shoot()
    {
        UnityEngine.Debug.Log("Shooting");
        if (Ammo == 0)
        {
            UnityEngine.Debug.Log(" no ammo reloading");
            await Reload();
        }
        else if (FireRateTimer.Elapsed < FireRate)
        {
            return;
        }
        else
        {
            Ammo--;
            if (Physics.Raycast(Holder.cameraPosition.position, Holder.cameraFoward, out RaycastHit hitInfo, Range, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                DrawLine(hitInfo.collider.gameObject.transform.position);
                if (hitInfo.collider.gameObject.TryGetComponent(out T script))
                {
                    script.ApplyDamage(Damage);
                }
            }
            else
            {
                DrawLine(Holder.cameraFoward * Range);
            }
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
    public float Range => 10;
    public override int MagizineSize => 3;
    public float Damage => 17;
    public override int Ammo { get; protected set; }
    public override bool IsReloading { get; protected set; }
    public TimeSpan FireRate => TimeSpan.FromSeconds(1);
    public Stopwatch FireRateTimer { get; } = new();
    public TimeSpan ReloadTime { get; } = TimeSpan.FromSeconds(0.5);
    public PhonePistol(IEntity player)
    {
        Holder = player;
        FireRateTimer.Start();
        Ammo = MagizineSize;
    }

    public override async void Shoot()
    {
        UnityEngine.Debug.Log("Shooting");
        if (Ammo == 0)
        {
            UnityEngine.Debug.Log(" no ammo reloading");
            await Reload();
        } else if (FireRateTimer.Elapsed < FireRate)
        {
            return;
        } else
        {
            Ammo--;
            if (Physics.Raycast(Holder.cameraPosition.position, Holder.cameraFoward, out RaycastHit hitInfo, Range, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                DrawLine(hitInfo.collider.gameObject.transform.position);
                if (hitInfo.collider.gameObject.TryGetComponent(out T script))
                {
                    script.ApplyDamage(Damage);
                }
            }
            else
            {
                DrawLine(Holder.cameraFoward * Range);
            }
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
    public float Range => 1000;
    public override int MagizineSize => 28;
    public float Damage => 4;
    public override int Ammo { get; protected set; }
    public override bool IsReloading { get; protected set; }

    public TimeSpan FireRate => TimeSpan.FromSeconds(0.5);
    public Stopwatch FireRateTimer { get; } = new();
    public TimeSpan ReloadTime { get; } = TimeSpan.FromSeconds(1);
    public FN(IEntity player)
    {
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
        } else if (FireRateTimer.Elapsed < FireRate)
        {
            return;
        } else
        {
            Ammo--;
            if (Physics.Raycast(Holder.cameraPosition.position, Holder.cameraFoward, out RaycastHit hitInfo, Range, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                DrawLine(hitInfo.collider.gameObject.transform.position);
                if (hitInfo.collider.gameObject.TryGetComponent(out T script))
                {
                    script.ApplyDamage(Damage);
                }
            }
            else
            {
                DrawLine(Holder.cameraFoward * Range);
            }
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