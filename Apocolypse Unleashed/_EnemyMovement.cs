using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class _EnemyMovement : MonoBehaviour
{
    // Head Components 
    public NavMeshAgent Agent;
    public Transform Player;
    public LayerMask WhatIsGround, WhatIsPlayer;

    // variable for walking
    public Vector3 _walkingPoint;
    public bool _walkingPointSet;
    public float _walkingPointRange;

    // Attacking Variables
    public float _timeBetweenAttacks;
    bool _alreadyAttacked;
    public float _sightRange, _attackRange;
    public bool _playerInSightRange, _playerInAttackRange;
    private bool _damageCoroutineStarted = false;   

    // Scripts
    _EnemyAnimationController _enemyAnimationController;
    _PlayerHealth PlayerHealth;
    _WaveSystem Spawner;

    //audio source
    [SerializeField] AudioSource EnemyAttackaudio;

    private void Awake()
    {
        Player = GameObject.Find("Player").transform;
        _enemyAnimationController= GetComponent<_EnemyAnimationController>();
        PlayerHealth = GameObject.FindObjectOfType<_PlayerHealth>();    
    }

    private void Update()
    {
        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, WhatIsPlayer);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange,WhatIsPlayer);

        if (!_playerInSightRange && !_playerInAttackRange) Patroling();
        if (_playerInSightRange && !_playerInAttackRange) ChasePlayer();
        if (_playerInAttackRange && _playerInSightRange) AttackPlayer();
    }
    private void Patroling()
    {
        _enemyAnimationController.Walk(); 

        if (!_walkingPointSet)
            SearchWalkPoint();
        if (_walkingPointSet)
            Agent.SetDestination(_walkingPoint);

        Vector3 distanceToWalkPoint = transform.position - _walkingPoint;
        Agent.speed = 1.5f;
        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 3f)
        {
            _walkingPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        // Generate random offsets within the walking point range
        Vector3 randomDirection = Random.insideUnitSphere * _walkingPointRange;
        randomDirection += transform.position;
        NavMeshHit hit;

        // Find a valid random point on the NavMesh within the walking point range
        if (NavMesh.SamplePosition(randomDirection, out hit, _walkingPointRange, NavMesh.AllAreas))
        {
            _walkingPoint = hit.position;
            _walkingPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        Agent.SetDestination(Player.position);
        _enemyAnimationController.Run();
        Agent.speed = 6;
    }

    private void StartAttackRoutine()
    {
        InvokeRepeating(nameof(AttackPlayer), 0f, 2f); // Invoke AttackPlayer() every 2 seconds
    }

    private void StopAttackRoutine()
    {
        CancelInvoke(nameof(AttackPlayer)); // Cancel the repeating invocation of AttackPlayer()
    }

    private void AttackPlayer()
    {
        _enemyAnimationController.Attack_1();
        // Check if the damage coroutine has already been started
        if (!_damageCoroutineStarted)
        {
            StartCoroutine(DamagePlayerCoroutine());
            _damageCoroutineStarted = true; // Set the flag to true to indicate that the coroutine has started
        }

        Agent.SetDestination(transform.position);
        transform.LookAt(Player);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
    }
    public IEnumerator DamagePlayerCoroutine()
    {
        yield return StartCoroutine(PlayerHealth.PlayerDamage(1));

        EnemyAttackaudio.Play();
        // This code will execute after the coroutine has completed
        if (!_alreadyAttacked)
        {
            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }
    public void SetSpawner(_WaveSystem _spawner) 
    {
        Spawner = _spawner;
        GetComponent<_EnemyHealth>().Wavesystem = _spawner;
    }

    private void ResetAttack()
    {
        _damageCoroutineStarted = false; // Reset the flag so that the damage coroutine can be started again
        _alreadyAttacked = false; // Reset the flag so that AttackPlayer() can attack again
    }
}
