using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float DownTime = 2.0f;
    public delegate void OnExecuteAction(string action);
    public OnExecuteAction OnExecute;

    private bool _isJumping = true;

    private void Awake()
    {
        OnExecute = ExecuteAction;
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
        if (collision.collider.gameObject.name == "Pavimento" && _isJumping)
        {
            _isJumping = false;
        }
    }

    private void ExecuteAction(string action)
    {

    }
}
