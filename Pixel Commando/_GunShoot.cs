using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void Damage(float damageAmount) 
    {
    

    }
}
public class _GunShoot : MonoBehaviour
{
    public string GunName;
    public int CurrentAmmo;
    public int MaxAmmo;
    public int Damage;
    public float ShootCoolDown;
    public float ReloadTime;
    public float ShootingRange = 15f;


    private bool _isShooting = false;
    private bool _isReloading = false;
    private bool _isShootingCoroutineRunning = false;
    [SerializeField] Transform _shootPos;
    [SerializeField] SpriteRenderer _gunSprite;

    public void StartShooting()
    {
        _isShooting = true;
        if (!_isShootingCoroutineRunning)
        {
            StartCoroutine(Shoot());
        }
    }
    public void StopShooting() 
    {
      _isShooting =false;
    }

    IEnumerator Shoot() 
    {
        _isShootingCoroutineRunning = true;
        while (_isShooting)
        {
            if (CurrentAmmo != 0 && !_isReloading)
            {
                RaycastHit2D hit = Physics2D.Raycast(_shootPos.position, _shootPos.right, 15f);
                Debug.DrawRay(_shootPos.position, _shootPos.right * 15, Color.magenta);
                CurrentAmmo--;
                if (hit.collider != null)
                {
                    IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        damageable.Damage(Damage); // Damage the object 
                    }
                }
                else
                {
                    Debug.Log("Niets geraakt");
                }

            }
            yield return new WaitForSeconds(ShootCoolDown);
        }
        _isShootingCoroutineRunning = false;
        yield return null;
    }
        

    // reload data
    IEnumerator ReloadGun()
    {
        if (CurrentAmmo!=MaxAmmo)
        {
            _isReloading = true;
            yield return new WaitForSeconds(ReloadTime);
            CurrentAmmo = MaxAmmo;
            _isReloading = false;
        }
        yield return null;
    }

    public void Reload() 
    {
        StartCoroutine(ReloadGun());
    }
}
