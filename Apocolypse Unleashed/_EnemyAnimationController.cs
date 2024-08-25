using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _EnemyAnimationController : MonoBehaviour
{
    public Animator _animator;

    // for running animation
    public void Run()
    {
        _animator.SetBool("Run", true);
    }
    // first attack animation
    public void Attack_1()
    {
        int randomAttack = Random.Range(0, 2);
        _animator.SetInteger("Selected Attack", 0);
        _animator.SetTrigger("Attack");
    }
    // second attack animation
    public void Attack_2()
    {
        _animator.Play("Attack2");
    }
    // idle for doing nothing animation
    public void Idle()
    {
        _animator.Play("Idle");
    }
    // for just walking animation
    public void Walk()
    {
        _animator.Play("Walk");
    }
    // enemy Death animation
    public void Die()
    {
        _animator.Play("Death");
    }
}
