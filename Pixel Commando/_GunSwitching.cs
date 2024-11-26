using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GunSwitching : MonoBehaviour
{
    // gun data 
    [SerializeField] _GunData _ak47;
    [SerializeField] _GunData _pistol;
    [SerializeField] _GunData _miniGun;

    private _GunData _currentGunData;

    [SerializeField] private _GunShoot _gunShoot;

    void Start()
    {
        StartCoroutine(SetGunData(_pistol));
    }

    IEnumerator SetGunData(_GunData newGunData)
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


    public void SwitchToPistol()
    {
        _currentGunData._currentAmmo = _gunShoot.CurrentAmmo;
        StartCoroutine(SetGunData(_pistol));
    }

    public void SwitchToAK47()
    {
        _currentGunData._currentAmmo = _gunShoot.CurrentAmmo;
        StartCoroutine(SetGunData(_ak47));

    }
    public void SwitchToMiniGun()
    {
        _currentGunData._currentAmmo = _gunShoot.CurrentAmmo;
        StartCoroutine(SetGunData(_miniGun));
    }
}
