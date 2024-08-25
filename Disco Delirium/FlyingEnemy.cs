using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour, IMovement
{    
    // speed variables for the enemy
    public float _speed;
    public float MaxSpeed;
    public float MinSpeed;

    public float _returnspeed;
    public float _randomPosTime;
    // offset for how far the enemy is removed from the player 
    public float _offsetY;
    public float _offsetX;
   
    public bool chase = false;
    private bool _calculatingRandomPos = true;

    public Transform StartingPoint;
    public GameObject player;
    public Vector2 _attackPos;
    private bool attack;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Setting the speed of the enemies to random 
        _speed = Random.Range(MinSpeed, MaxSpeed);
    }

    private void Update()
    {
        if (!attack)
        {
            // every 2 seconds the enemy generates a new random pos
            if (_calculatingRandomPos)
            {
                StartCoroutine(CalculateOffset());
                _calculatingRandomPos = false;
            }

            if (player == null)
                return;
            if (chase == true)

                Chase();
            else
                ReturnStartingPosition();
            Flip();
        }
            
    }


    
    public void Chase() 
    {
        _attackPos.y = player.transform.position.y + _offsetY;
        _attackPos.x = player.transform.position.x + _offsetX;

        transform.position = Vector2.MoveTowards(transform.position,_attackPos, _speed * Time.deltaTime);
    }


    private IEnumerator CalculateOffset() 
    {
        Debug.Log(gameObject.name + " calculated offset");
        // calculate player Offset for the enemy 
        _offsetX = Random.Range(-2, 2);
        _offsetY = Random.Range(2, 6);

        // _calculatingRandomPos = true;
        yield return new WaitForSeconds(_randomPosTime);

        _calculatingRandomPos = true;

        _randomPosTime = Random.Range(1, 4);
        
        yield return null;
    } 

    private void ReturnStartingPosition() 
    {
        transform.position = Vector3.MoveTowards(transform.position, StartingPoint.position, _returnspeed * Time.deltaTime);   
    }
    private void Flip() 
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void Stop()
    {
        attack = true;
    }
}
