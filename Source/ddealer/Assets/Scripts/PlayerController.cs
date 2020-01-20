using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _jumpForce = 300.0f;
    private bool _isDead = false;
    private bool _isJumping = true;
    private Rigidbody2D _playerRigidBody;

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

        if (Input.GetMouseButtonDown(0) && !_isJumping)
        {
            Jump();
        }
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
    }

    private void Die()
    {
        _isDead = true;
    }

    private void Jump()
    {
        _isJumping = true;
        _playerRigidBody.velocity = Vector2.zero;
        _playerRigidBody.AddForce(new Vector2(0, _jumpForce));
    }
}
