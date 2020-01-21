using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool _isJumping = true;
    private float _jumpForce = 300.0f;
    private Rigidbody2D _enemyRigidBody;
    private float _initialX;

    private void Awake()
    {
        _enemyRigidBody = GetComponent<Rigidbody2D>();
        _initialX = gameObject.transform.position.x;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FreezePosX();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Floor") && _isJumping)
            _isJumping = false;

        //if (collision.collider.gameObject.name == "Jumper" && collision.collider.isTrigger && !_isJumping)
        if (collision.collider.gameObject.name == "Jumper")
            Jump();
    }

    private void Jump()
    {
        _isJumping = true;
        _enemyRigidBody.velocity = Vector2.zero;
        _enemyRigidBody.AddForce(new Vector2(0, _jumpForce));
    }

    private void FreezePosX()
    {
        var position = gameObject.transform.position;

        if (position.x != _initialX)
        {
            gameObject.transform.position = new Vector3(_initialX, gameObject.transform.position.y, 0);
        }
    }
}
