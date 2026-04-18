using UnityEngine;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

public interface IPaint
{
    string Name { get; }
    int Ammo { get; }
    int MagizineSize { get; }
    bool IsReloading { get; }
    float Range { get; }
    float Damage { get; }

    TimeSpan FireRate { get; }
    Stopwatch FireRateTimer { get; }
    void Shoot();
    Task Reload();
}

public class Barrett : IPaint
{
    private IHostile _Player { get; set; }
    public string Name => "Barrett M82";
    public float Range => 80;
    public int MagizineSize => 5;
    public float Damage => 80;
    public int Ammo { get; private set; }
    public bool IsReloading { get; private set; }

    public TimeSpan FireRate => TimeSpan.FromSeconds(1);
    public Stopwatch FireRateTimer { get; } = new();
    public Barrett(IHostile player)
    {
        _Player = player;
        FireRateTimer.Start();
    }

    public async void Shoot()
    {
        
        if (Ammo == 0)
        {
            await Reload();
        } else if (FireRateTimer.Elapsed < FireRate)
        {
            return;
        } else 
        {
            Ammo--;
            if (Physics.Raycast(_Player.transform.position, _Player.cameraPosition.forward, out RaycastHit hit, Range))
            {
                if (hit.collider.gameObject.TryGetComponent<IHostile>(out IHostile script))
                {
                    script.ApplyDamage(Damage);
                }
            }
        }
        FireRateTimer.Restart();
    }

    public async Task Reload()
    {
        await Task.Delay(500);
        Ammo = (Ammo > 0) ? MagizineSize + 1 : MagizineSize;
    }
}

public class AR : IPaint
{
    private IHostile _Player { get; set; }
    public string Name => "AR-57";
    public float Range => 30;
    public int MagizineSize => 25;
    public float Damage => 26;
    public int Ammo { get; private set; }
    public bool IsReloading { get; private set; }

    public TimeSpan FireRate => TimeSpan.FromSeconds(0.5);
    public Stopwatch FireRateTimer { get; } = new();
    public AR(IHostile player)
    {
        _Player = player;
        FireRateTimer.Start();
    }

    public async void Shoot()
    {

        if (Ammo == 0)
        {
            await Reload();
        } else if (FireRateTimer.Elapsed < FireRate)
        {
            return;
        } else
        {
            Ammo--;
            if (Physics.Raycast(_Player.transform.position, _Player.cameraPosition.forward, out RaycastHit hit, Range))
            {
                if (hit.collider.gameObject.TryGetComponent<IHostile>(out IHostile script))
                {
                    script.ApplyDamage(Damage);
                }
            }
        }
        FireRateTimer.Restart();
    }

    public async Task Reload()
    {
        await Task.Delay(1000 * (17/60));
        Ammo = (Ammo > 0) ? MagizineSize + 1 : MagizineSize;
    }
}

public class PhonePistol : IPaint
{
    private IHostile _Player { get; set; }
    public string Name => "Ideal Conceal";
    public float Range => 10;
    public int MagizineSize => 3;
    public float Damage => 17;
    public int Ammo { get; private set; }
    public bool IsReloading { get; private set; }

    public TimeSpan FireRate => TimeSpan.FromSeconds(1);
    public Stopwatch FireRateTimer { get; } = new();
    public PhonePistol(IHostile player)
    {
        _Player = player;
    }

    public async void Shoot()
    {

        if (Ammo == 0)
        {
            await Reload();
        } else if (FireRateTimer.Elapsed < FireRate)
        {
            return;
        } else
        {
            Ammo--;
            if (Physics.Raycast(_Player.transform.position, _Player.cameraPosition.forward, out RaycastHit hit, Range))
            {
                if (hit.collider.gameObject.TryGetComponent<IHostile>(out IHostile script))
                {
                    script.ApplyDamage(Damage);
                }
            }
        }
    }

    public async Task Reload()
    {
        await Task.Delay(500);
        Ammo =  MagizineSize;
    }
}

public class FN : IPaint
{
    private IHostile _Player { get; set; }
    public string Name => "FN 510";
    public float Range => 1000;
    public int MagizineSize => 28;
    public float Damage => 20;
    public int Ammo { get; private set; }
    public bool IsReloading { get; private set; }

    public TimeSpan FireRate => TimeSpan.FromSeconds(0.2);
    public Stopwatch FireRateTimer { get; } = new();
    public FN(IHostile player)
    {
        _Player = player;
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
            UnityEngine.Debug.Log("try fire");
            Ammo--;
            foreach (RaycastHit hit in Physics.RaycastAll(_Player.transform.position, _Player.cameraPosition.forward, Range))
            {
                UnityEngine.Debug.Log("checking hit");
                if (hit.collider.gameObject == _Player.cameraPosition.gameObject) continue;
                UnityEngine.Debug.Log("Valid hit");
                if (hit.collider.gameObject.TryGetComponent<IHostile>(out IHostile script))
                {
                    UnityEngine.Debug.Log("Damaging");
                    script.ApplyDamage(Damage);
                    break;
                }
            }
        }
        FireRateTimer.Restart();
    }

    public async Task Reload()
    {
        await Task.Delay(1000 * (17 / 60));
        Ammo = (Ammo > 0) ? MagizineSize + 1 : MagizineSize;
    }
}