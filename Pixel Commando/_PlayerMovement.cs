using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class _PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Player;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpingPower;

    public bool _moveLeft = false; 
    public bool _moveRight = false;
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private bool _jump = false;
    [SerializeField] bool _facingRight;


    private void Update()
    {
        // move left
        if (_moveLeft)
        {
            Player.transform.Translate(Vector2.left * _moveSpeed * Time.deltaTime);
            if (_facingRight) 
            {
                Flip();
            }
            _facingRight = false;
        }
        // move right
        if (_moveRight)
        {
            Player.transform.Translate(Vector2.left * _moveSpeed * Time.deltaTime);
            if (!_facingRight)  
            {
                Flip();
            }
            _facingRight = true;
        }
    }


    // fixedupdate used for consistent output not dependent on fps a
    // jump
    private void FixedUpdate()
    {
        if (_jump &&_isGrounded)
        {
            Player.AddForce(new Vector2(Player.velocity.x, _jumpingPower * 10));

            _jump = false;
            _isGrounded = false;
        }
    }

    //checking if player is on the ground
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            // Checking if objects are not rotated 
            Vector3 normal = other.GetContact(0).normal;
            if (normal == Vector3.up)
            {
                _isGrounded = true;
            }
        }
    }
    // leaving the collider
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    // trigger for start moving right
    public void MoveRight()
    {
        _moveRight = true;
    }
    // trigger for stop moving right
    public void StopMoveRight()
    {
        _moveRight = false;
    }
    // trigger for start moving left 
    public void MoveLeft()
    {
        _moveLeft = true;
    }
    // trigger for stop moving left
    public void StopMoveLeft()
    {
        _moveLeft = false;
    }
    public void Jump()
    {
        _jump = true;
    }

    // flipping the direction of the player 
    public void Flip()
    {
        _facingRight = !_facingRight; 
        transform.Rotate(0f, 180f, 0f);
    }

}
