using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName ="Guns")] 
public class _GunData : ScriptableObject
{
    public string _gunName;
    public int _currentAmmo;
    public int _maxAmmo;
    public int _damage;
    public float _shootCoolDown;
    public float _reloadTime;
    public float _shootingRange = 15f;



}
