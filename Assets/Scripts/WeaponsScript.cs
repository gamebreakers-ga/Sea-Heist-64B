using UnityEngine;
using System.Threading.Tasks;
using System;
public interface IWeapon
{
    string Name { get; }
    int Ammo { get; }
    int MagizineSize { get; }
    bool IsReloading { get; }
    float Range { get; }
    float Damage { get; }

    void Shoot();
    Task Reload();
}


public class Barrett : IWeapon
{
    private FirstPersonControllerScript _Player { get; set; }
    private GameObject _Bullet { get; set; }
    public string Name => "Barrett M82";
    public float Range => 80;
    public int MagizineSize => 5;
    public float Damage => 80;
    public int Ammo { get; private set; }
    public bool IsReloading { get; private set; }

    public Barrett(GameObject bullet, FirstPersonControllerScript player)
    {
        _Bullet = bullet;
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
            GameObject bullet = GameObject.Instantiate(_Bullet, _Player.cameraPosition.position + _Player.cameraPosition.forward, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Damage = Damage;
            Vector3 startPos = _Player.cameraPosition.position;
            await GF.Until(() => Vector3.Distance(startPos, bullet.transform.position) < 200, 2000);
        }
    }

    public async Task Reload()
    {
        await Task.Delay(2000);
        Ammo = (Ammo > 0) ? MagizineSize + 1 : MagizineSize;
    }
}