using UnityEngine;

public class PoolingBehaviour : MonoBehaviour
{
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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collidedObj = collision.gameObject;
        if (collidedObj.name.Contains("Obstacle"))
        {
            Destroy(collidedObj.transform.parent.gameObject);
        }
    }
}
