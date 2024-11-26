using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _EnemyHealth : MonoBehaviour, IDamageable
{
    public int Health = 10;

    private void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        Health--;
    }

    public void Damage(float damage)
    {
        Health--;
    }
}
