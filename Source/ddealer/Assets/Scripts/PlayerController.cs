using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject _obstacle;
    private bool _isDead = false;
    private bool _isJumping = true;
    private bool _isSlowing = false;
    private Rigidbody2D _playerRigidBody;
    private float _initialX;
    private SpriteRenderer _spriteRenderer;
    private Coroutine _fadingRoutine;

    private void Awake()
    {
        _initialX = gameObject.transform.position.x;
        _spriteRenderer = GetComponent<SpriteRenderer>();
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

        if (Input.GetKeyDown(KeyCode.Space) && !_isJumping && !_isSlowing)
            Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Floor") && _isJumping)
        {
            _isJumping = false;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !_isSlowing)
        {
            _obstacle = collision.gameObject;
            Slow();
        }
    }

    /// <summary>
    /// MAkes the player fall back disabling the Pos X constraint on the rigidbody
    /// </summary>
    private void Slow()
    {
        _playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        _isSlowing = true;
        _fadingRoutine = StartCoroutine(InvincibilityFade());
        Invoke("FreezePosX", .8f);
    }

    /// <summary>
    /// Create some sort of "blink" by fading in and out really fast
    /// </summary>
    /// <returns></returns>
    IEnumerator InvincibilityFade()
    {
        for (; ;)
        {
            Color c = _spriteRenderer.color;
            if (c.a > 0)
            {
                for (float ft = 1f; ft >= 0; ft -= 0.1f)
                {
                    c.a = ft;
                    _spriteRenderer.color = c;
                    yield return null;
                }
            }
            else
            {
                for (float ft = 0.1f; ft <= 1; ft += 0.1f)
                {
                    c.a = ft;
                    _spriteRenderer.color = c;
                    yield return null;
                }
            }
        }
    }

    private void Die()
    {
        _isDead = true;
        GameUIManager.Instance.ShowDeathMenu();
        GameController.Instance.GameOver = true;
    }

    private void Jump()
    {
        _playerRigidBody.velocity = Vector2.zero;
        _playerRigidBody.AddForce(new Vector2(0, GameController.Instance.JumpForce));
        _isJumping = true;
    }

    /// <summary>
    /// Reenable player input and Pos X constraint
    /// </summary>
    private void FreezePosX()
    {
        if (_playerRigidBody.constraints != RigidbodyConstraints2D.FreezePositionX)
            _playerRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;

        _isSlowing = false;
        StopCoroutine(_fadingRoutine);
    }

    private void ResetSpriteAlpha()
    {
        if (_spriteRenderer.color.a != 1f)
        {
            Color color = _spriteRenderer.color;
            color.a = 1f;
            _spriteRenderer.color = color;
        }
    }
}
