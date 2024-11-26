using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitching : MonoBehaviour
{
    // gun data 
    [SerializeField] GunData _ak47;
    [SerializeField] GunData _pistol;
    [SerializeField] GunData _miniGun;

    private GunData _currentGunData;

    [SerializeField] private GunShoot _gunShoot;

    void Start()
    {
        StartCoroutine(SetGunData(_pistol));
    }

    // setting the different gun data to the current data
    IEnumerator SetGunData(GunData newGunData)
    {
        _currentGunData = newGunData;

        _gunShoot.GunName = _currentGunData._gunName;
        _gunShoot.CurrentAmmo = _currentGunData._currentAmmo;
        _gunShoot.MaxAmmo = _currentGunData._maxAmmo;
        _gunShoot.Damage = _currentGunData._damage;
        _gunShoot.ShootCoolDown = _currentGunData._shootCoolDown;
        _gunShoot.ReloadTime = _currentGunData._reloadTime;
        _gunShoot.ShootingRange = _currentGunData._shootingRange;

        yield return null;
    }

    // switching to pistol and setting the ammo from the previous gun
    public void SwitchToPistol()
    {
        _currentGunData._currentAmmo = _gunShoot.CurrentAmmo;
        StartCoroutine(SetGunData(_pistol));
    }
    // switching to pistol and setting the ammo from the previous gun
    public void SwitchToAK47()
    {
        _currentGunData._currentAmmo = _gunShoot.CurrentAmmo;
        StartCoroutine(SetGunData(_ak47));
    }
    // switching to pistol and setting the ammo from the previous gun
    public void SwitchToMiniGun()
    {
        _currentGunData._currentAmmo = _gunShoot.CurrentAmmo;
        StartCoroutine(SetGunData(_miniGun));
    }
}
