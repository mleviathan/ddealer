using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Obstacle;
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
            JumpObstacleAndSlow();
        }
    }

    private void JumpObstacleAndSlow()
    {
        var obstacleSize = Obstacle.GetComponent<BoxCollider2D>().size.x;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + obstacleSize, gameObject.transform.position.y, 0);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine("ReactivateFreezePosX");
    }

    IEnumerable ReactivateFreezePosX()
    {
        yield return new WaitForSeconds(1f);
        FreezePosX();
        StopAllCoroutines();
    }

    private void Die()
    {
        _isDead = true;
        GameController.Instance.GameOver = true;
    }

    private void Jump()
    {
        _playerRigidBody.velocity = Vector2.zero;
        _playerRigidBody.AddForce(new Vector2(0, GameController.Instance.JumpForce));
        _isJumping = true;
    }

    private void FreezePosX()
    {
        if (GetComponent<Rigidbody2D>().constraints != RigidbodyConstraints2D.FreezePositionX)
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
    }
}
