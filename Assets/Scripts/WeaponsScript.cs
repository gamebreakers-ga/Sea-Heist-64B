using UnityEngine;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

public interface IPaint
{

}

public interface IPaint<T> : IPaint where T : IEntity
{
    string Name { get; }
    int Ammo { get; }
    int MagizineSize { get; }
    bool IsReloading { get; }
    float Range { get; }
    float Damage { get; }
    TimeSpan FireRate { get; }
    Stopwatch FireRateTimer { get; }
    TimeSpan ReloadTime { get; }
    void Shoot();
    Task Reload();
}

public class Barrett<T> : IPaint<T> where T : IEntity
{
    private IEntity _Holder { get; set; }
    public string Name => "Barrett M82";
    public float Range => 80;
    public int MagizineSize => 5;
    public float Damage => 80;
    public int Ammo { get; private set; }
    public bool IsReloading { get; private set; }
    public TimeSpan FireRate => TimeSpan.FromSeconds(1);
    public Stopwatch FireRateTimer { get; } = new();
    public TimeSpan ReloadTime { get; } = TimeSpan.FromSeconds(0.5);

    public Barrett(IEntity player)
    {
        _Holder = player;
        FireRateTimer.Start();

        Ammo = MagizineSize;
    }

    public async void Shoot()
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
            foreach (RaycastHit hit in Physics.RaycastAll(_Holder.cameraPosition.position, _Holder.cameraFoward, Range))
            {
                if (hit.collider.gameObject.TryGetComponent(out T script))
                {
                    script.ApplyDamage(Damage);
                    break;
                }
            }
        }
        FireRateTimer.Restart();
    }

    public async Task Reload()
    {
        if (IsReloading) return; 
        await Task.Delay(ReloadTime);
        Ammo = (Ammo > 0) ? MagizineSize + 1 : MagizineSize;
    }
}

public class AR<T> : IPaint<T> where T : IEntity
{
    private IEntity _Holder { get; set; }
    public string Name => "AR-57";
    public float Range => 30;
    public int MagizineSize => 25;
    public float Damage => 26;
    public int Ammo { get; private set; }
    public bool IsReloading { get; private set; }
    public TimeSpan FireRate => TimeSpan.FromSeconds(0.5);
    public Stopwatch FireRateTimer { get; } = new();
    public TimeSpan ReloadTime { get; } = TimeSpan.FromSeconds(0.5);

    public AR(IEntity player)
    {
        _Holder = player;
        FireRateTimer.Start();
        Ammo = MagizineSize;
    }

    public async void Shoot()
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
            foreach (RaycastHit hit in Physics.RaycastAll(_Holder.cameraPosition.position, _Holder.cameraFoward, Range))
            {
                if (hit.collider.gameObject.TryGetComponent(out T script))
                {
                    script.ApplyDamage(Damage);
                    break;
                }
            }
        }
        FireRateTimer.Restart();
    }

    public async Task Reload()
    {
        if (IsReloading) return;
        await Task.Delay(ReloadTime);
        Ammo = (Ammo > 0) ? MagizineSize + 1 : MagizineSize;
    }
}

public class PhonePistol<T> : IPaint<T> where T : IEntity
{
    private IEntity _Holder { get; set; }
    public string Name => "Ideal Conceal";
    public float Range => 10;
    public int MagizineSize => 3;
    public float Damage => 17;
    public int Ammo { get; private set; }
    public bool IsReloading { get; private set; }
    public TimeSpan FireRate => TimeSpan.FromSeconds(1);
    public Stopwatch FireRateTimer { get; } = new();
    public TimeSpan ReloadTime { get; } = TimeSpan.FromSeconds(0.5);
    public PhonePistol(IEntity player)
    {
        _Holder = player;
        FireRateTimer.Start();
        Ammo = MagizineSize;
    }

    public async void Shoot()
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
            foreach (RaycastHit hit in Physics.RaycastAll(_Holder.cameraPosition.position, _Holder.cameraFoward, Range))
            {
                if (hit.collider.gameObject.TryGetComponent(out T script))
                {
                    script.ApplyDamage(Damage);
                    break;
                }
            }
        }
        FireRateTimer.Restart();
    }

    public async Task Reload()
    {
        if (IsReloading) return;
        await Task.Delay(ReloadTime);
        Ammo =  MagizineSize;
    }
}

public class FN<T> : IPaint<T> where T : IEntity
{
    private IEntity _Holder { get; set; }
    public string Name => "FN 510";
    public float Range => 1000;
    public int MagizineSize => 28;
    public float Damage => 4;
    public int Ammo { get; private set; }
    public bool IsReloading { get; private set; }

    public TimeSpan FireRate => TimeSpan.FromSeconds(0.5);
    public Stopwatch FireRateTimer { get; } = new();
    public TimeSpan ReloadTime { get; } = TimeSpan.FromSeconds(1);
    public FN(IEntity player)
    {
        _Holder = player;
        FireRateTimer.Start();
        Ammo = MagizineSize;
    }

    public async void Shoot()
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
            foreach (RaycastHit hit in Physics.RaycastAll(_Holder.cameraPosition.position, _Holder.cameraFoward, Range))
            {
                if (hit.collider.gameObject.TryGetComponent(out T script))
                {
                    script.ApplyDamage(Damage);
                    break;
                }
            }
        }
        FireRateTimer.Restart();
    }

    public async Task Reload()
    {
        if (IsReloading) return;
        await Task.Delay(ReloadTime);
        Ammo = (Ammo > 0) ? MagizineSize + 1 : MagizineSize;
    }
}