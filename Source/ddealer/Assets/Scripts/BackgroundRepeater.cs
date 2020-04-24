using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BackgroundRepeater : MonoBehaviour
{
    private BoxCollider2D _groundCollider;        
    private float _groundHorizontalLength;        

    //Awake is called before Start.
    private void Awake()
    {
        _groundCollider = this.GetComponent<BoxCollider2D>();
        _groundHorizontalLength = _groundCollider.size.x * 1.5f; // Fine tuned for our backgrounds
    }

    //Update runs once per frame
    private void Update()
    {
        if (transform.position.x < -_groundHorizontalLength)
        {
            RepositionBackground();
        }
    }

    private void RepositionBackground()
    {
        Vector2 groundOffSet = new Vector2(_groundHorizontalLength * 2.5f, 0); // Fine tuned for our backgrounds

        transform.position = (Vector2)transform.position + groundOffSet;
    }
}
