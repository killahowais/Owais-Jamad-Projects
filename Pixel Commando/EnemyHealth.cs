using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int Health = 10;

    private void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    // hits
    public void Hit()
    {
        Health--;
    }

    // damage 
    public void Damage(float damage)
    {
        Health--;
    }
}
