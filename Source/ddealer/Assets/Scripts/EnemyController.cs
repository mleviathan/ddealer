using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool _isJumping = true;
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Floor") && _isJumping)
            _isJumping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isJumping)
            Jump();
    }

    private void Jump()
    {
        _enemyRigidBody.velocity = Vector2.zero;
        _enemyRigidBody.AddForce(new Vector2(0, GameController.Instance.JumpForce));
        _isJumping = true;
    }

}
