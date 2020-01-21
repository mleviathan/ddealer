using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBehaviour : MonoBehaviour
{
    public float ScrollSpeed = -3f;

    private Rigidbody2D _floorRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _floorRigidBody = GetComponent<Rigidbody2D>();
        _floorRigidBody.velocity = new Vector2(ScrollSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.GameOver)
        {
            _floorRigidBody.velocity = Vector2.zero;
        }
    }
}
