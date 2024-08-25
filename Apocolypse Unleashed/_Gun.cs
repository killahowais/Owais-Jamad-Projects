using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _Gun : MonoBehaviour
{
    // Shootflaire for The gun
    public Camera FpsCam;
    public ParticleSystem ShootFlaire;
    // tweakable variables for how much damage how far and how fast the gun shoots
    public float _damage = 10f;
    public float _range = 100f;
    public float _fireRate = 15f;
    //variables To not change 
    private bool _reloading;
    private float _nextTimeToFire = 0f;
    private float _gunReloadTime = 3.5f;
    private float _ShootCoolDown = 2;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private int _Currentammo;
    // Text variable
    public TMP_Text _ammoText;
    public Animator _animator;
    // audio system
   public AudioSource Shootaudio;
    public AudioSource ReloadAudio;


    // Getting full magazine when starting 
    private void Start()
    {
        _Currentammo = _maxAmmo;
    }
    #region
    // Function for
    // :Checking Input for Firing
    // :Checking Input for Reloading
    #endregion 
    private void Update()
    {
        // when pressing left mouse button and nextimetofire bigger or equal is then time  and the gun isn't reloading 
        if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire && !_reloading)
        {
            if (_Currentammo > 0)
            { 
                _nextTimeToFire = Time.time + 1f / _fireRate;
                Shoot();
                _Currentammo--;
            }
            else if (_Currentammo < 0)
            {
                _Currentammo = 0;
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            _animator.Play("Aiming");
        }
        if (Input.GetButtonUp("Fire2"))
        {
            _animator.Play("StopAiming");
        }
        // If R is pressed ammo is not full and is already reloading is false 
        if (Input.GetKeyDown(KeyCode.R) && _Currentammo != _maxAmmo && _reloading==false)
        {
            // Reload gun
            StartCoroutine(Reload());
        }

        // Displaying CurrentAmmo Text
        _ammoText.text ="Ammo: "+ _Currentammo.ToString("D2" );
    }

    // shoot function
    private void Shoot()
    {
        ShootFlaire.Play();
        Shootaudio.Play();
        RaycastHit hit;
        if ( Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out hit))
        {
            // for debugging 
           // Debug.Log(hit.transform.name);
            _EnemyHealth Enemyhealth = hit.transform.GetComponent<_EnemyHealth>();
            if (Enemyhealth != null)
            {
                Enemyhealth.TakeDamage(_damage);
            }
        }
    }

    // reloading 
    public IEnumerator Reload()
    {
        ReloadAudio.Play();
        _reloading= true;
        yield return new WaitForSeconds(_gunReloadTime);
        _Currentammo = _maxAmmo;
        _reloading = false;
    }
}