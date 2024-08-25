using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttacking : MonoBehaviour
{
    private GameObject _player;
    private bool _hitting;
    private Animator _animator;
    private float _distanceToPlayer;
    private TurnAround _turnAround;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
        _turnAround = GetComponent<TurnAround>();
    }

    private void Update()
    {
        _distanceToPlayer = Vector2.Distance(_player.transform.position, transform.position);

        if (_distanceToPlayer < 3.5f && !_hitting)
        {
            StartCoroutine(Hit());
        }
    }

    private IEnumerator Hit()
    {
        _hitting = true;
        _turnAround.IsHitting = true;

        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        _animator.SetTrigger("Hitting");
        if (_player.transform.position.x > transform.position.x)
            _turnAround.WhereHit = false;
        else 
            _turnAround.WhereHit = true;

        if (_distanceToPlayer < 2.5f)
        {
            _player.GetComponent<PlayerHealth>().TakeDamage(20);
        }
        yield return new WaitForSeconds(0.3f);
        yield return new WaitForSeconds(2f);
        _turnAround.IsHitting = false;
        _hitting = false;
    }
}
