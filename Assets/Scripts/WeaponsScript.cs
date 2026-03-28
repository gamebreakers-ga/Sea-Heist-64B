using UnityEngine;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
public interface IPaint
{
    string Name { get; }
    int Ammo { get; }
    int MagizineSize { get; }
    bool IsReloading { get; }
    float Range { get; }
    float Damage { get; }

    float FireRate { get; }
    Stopwatch FireRateTimer { get; }
    void Shoot();
    Task Reload();
}

public class Barrett : IPaint
{
    private FirstPersonControllerScript _Player { get; set; }
    public string Name => "Barrett M82";
    public float Range => 80;
    public int MagizineSize => 5;
    public float Damage => 80;
    public int Ammo { get; private set; }
    public bool IsReloading { get; private set; }

    public float FireRate => 1000;
    public Stopwatch FireRateTimer { get; } = new();
    public Barrett(FirstPersonControllerScript player)
    {
        _Player = player;
        FireRateTimer.Start();
    }

    public async void Shoot()
    {
        if (FireRateTimer.ElapsedMilliseconds < FireRate) return;
        
        if (Ammo == 0)
        {
            await Reload();
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
    private FirstPersonControllerScript _Player { get; set; }
    public string Name => "AR-57";
    public float Range => 30;
    public int MagizineSize => 25;
    public float Damage => 26;
    public int Ammo { get; private set; }
    public bool IsReloading { get; private set; }

    public float FireRate => 1000 * (15/60);
    public Stopwatch FireRateTimer { get; } = new();
    public AR( FirstPersonControllerScript player)
    {
        _Player = player;
        FireRateTimer.Start();
    }

    public async void Shoot()
    {
        if (FireRateTimer.ElapsedMilliseconds < FireRate) return;

        if (Ammo == 0)
        {
            await Reload();
        }
        else
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
    private FirstPersonControllerScript _Player { get; set; }
    public string Name => "Ideal Conceal";
    public float Range => 10;
    public int MagizineSize => 3;
    public float Damage => 17;
    public int Ammo { get; private set; }
    public bool IsReloading { get; private set; }

    public float FireRate => default;
    public Stopwatch FireRateTimer => null;
    public PhonePistol(FirstPersonControllerScript player)
    {
        _Player = player;
    }

    public async void Shoot()
    {

        if (Ammo == 0)
        {
            await Reload();
        }
        else
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
    private FirstPersonControllerScript _Player { get; set; }
    public string Name => "FN 510";
    public float Range => 20;
    public int MagizineSize => 28;
    public float Damage => 20;
    public int Ammo { get; private set; }
    public bool IsReloading { get; private set; }

    public float FireRate => 1000 * (9/60);
    public Stopwatch FireRateTimer { get; } = null;
    public FN(FirstPersonControllerScript player)
    {
        _Player = player;
    }

    public async void Shoot()
    {
        if (FireRateTimer.ElapsedMilliseconds < FireRate) return;

        if (Ammo == 0)
        {
            await Reload();
        }
        else
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
        await Task.Delay(1000 * (17 / 60));
        Ammo = (Ammo > 0) ? MagizineSize + 1 : MagizineSize;
    }
}