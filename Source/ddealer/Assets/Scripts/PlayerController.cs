using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Obstacle;
    private float _jumpForce = 300.0f;
    private bool _isDead = false;
    private bool _isJumping = true;
    private bool _isSlowing = false;
    private Rigidbody2D _playerRigidBody;
    private float _initialX;

    private void Awake()
    {
        _initialX = gameObject.transform.position.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
            Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Floor") && _isJumping)
        {
            _isJumping = false;
        }

        if (collision.collider.gameObject.CompareTag("Enemy"))
        {
            Die();
        }

        if (collision.collider.gameObject.CompareTag("Obstacle"))
        {
            _isSlowing = true;
            var obstacleSize = Obstacle.GetComponent<BoxCollider2D>().size.x;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + obstacleSize, gameObject.transform.position.y, 0);
        }
    }

    private void Die()
    {
        _isDead = true;
    }

    private void Jump()
    {
        _isJumping = true;
        //_playerRigidBody.velocity = Vector2.zero;
        _playerRigidBody.AddForce(new Vector2(0, _jumpForce));
    }

    private void FreezePosX()
    {
        if (GetComponent<Rigidbody2D>().constraints != RigidbodyConstraints2D.FreezePositionX)
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
    }
}
