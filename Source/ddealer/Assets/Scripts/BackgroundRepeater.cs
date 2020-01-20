using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeater : MonoBehaviour
{
    private BoxCollider2D _groundCollider;        
    private float _groundHorizontalLength;        

    //Awake is called before Start.
    private void Awake()
    {
        _groundCollider = GetComponent<BoxCollider2D>();
        _groundHorizontalLength = _groundCollider.size.x;
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
        Vector2 groundOffSet = new Vector2(_groundHorizontalLength * 2f, 0);

        transform.position = (Vector2)transform.position + groundOffSet;
    }
}
