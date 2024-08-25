using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _EnemyHealth : MonoBehaviour
{
    _EnemyAnimationController EnemyAnimationController;
    public _WaveSystem Wavesystem;

    public float health = 100f;

    // audio clip
   public AudioSource Enemydeathsound;


    private void Start()
    {
        EnemyAnimationController = GetComponent<_EnemyAnimationController>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            StartCoroutine(EnemyDeath());
        }
    }

    // death animation and die logic 
    private IEnumerator EnemyDeath()
    {
        // Check if EnemyAnimationController is null before accessing its fields/methods
        if (EnemyAnimationController != null)
        {
            EnemyAnimationController._animator.Play("Death");
            Enemydeathsound.Play();
        }
        else
        {
            Debug.LogError("EnemyAnimationController is null!");
        }

        // Wait for 1 second before destroying the GameObject
        yield return new WaitForSeconds(1f);
        if (Wavesystem != null) Wavesystem._currentMonster.Remove(this.gameObject);

        
        // Destroy the GameObject
        Destroy(gameObject);
    }
}
