using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour, IDamagable
{
  //  public EnemyRoom Enemyroom;

    [SerializeField] private int maxHealth;
    public bool IsHitFlashing;
    private SpriteRenderer _sr;
    public int currentHealth;
   // [SerializeField] private ParticleSystem particlesystem;
    private Animator _animator;
    [SerializeField] private bool shouldDamage;
    [SerializeField] private GameObject textPopup;
    private MoveBetweenTo _moveBetweenTo;

    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        _moveBetweenTo = GetComponent<MoveBetweenTo>();
        _sr = GetComponent<SpriteRenderer>();
    }

    public void Hit(int amount)
    {
        currentHealth -= amount;
        if (!IsHitFlashing)
            StartCoroutine(HitFlash());
    }

    private IEnumerator HitFlash()
    {
        if (currentHealth < 1)
        {
            yield return new WaitForSeconds(.2f);
            _animator.SetTrigger("Dead");
            StartCoroutine(Death());
        }
        else
        {
            _animator.SetTrigger("Hit");
            _moveBetweenTo.enabled = false;
            yield return new WaitForSeconds(.5f);
            _moveBetweenTo.enabled = true;
        }
    }

    private IEnumerator Death()
    {
        _animator.SetTrigger("Death");
        if (_moveBetweenTo)
            _moveBetweenTo.enabled = false;
        if (textPopup)
            Instantiate(textPopup, transform.position + Vector3.up, quaternion.identity).GetComponent<TextPopup>().SelectedSprite(Random.Range(1, 3));
        //particlesystem.Play();
        if (TryGetComponent(out IMovement iMovement))
            iMovement.Stop();
        yield return new WaitForSeconds(1f);
        print("Dead");
        Destroy(gameObject);

        yield return null;
    }

}