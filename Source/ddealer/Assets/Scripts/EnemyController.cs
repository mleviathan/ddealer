using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool _isJumping = true;
    private float _jumpForce = 300.0f;
    private Rigidbody2D _enemyRigidBody;

    private void Awake()
    {
        _enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Floor") && _isJumping)
            _isJumping = false;

        if (collision.collider.gameObject.name == "Jumper" && collision.collider.isTrigger && !_isJumping)
            Jump();
    }

    private void Jump()
    {
        _isJumping = true;
        _enemyRigidBody.velocity = Vector2.zero;
        _enemyRigidBody.AddForce(new Vector2(0, _jumpForce));
    }
}
