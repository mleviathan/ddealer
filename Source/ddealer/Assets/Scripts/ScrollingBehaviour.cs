using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBehaviour : MonoBehaviour
{
    public delegate void IncreaseDifficulty();
    public IncreaseDifficulty OnIncreaseDifficulty;

    private List<Rigidbody2D> _floorsRigidBody = new List<Rigidbody2D>();
    private List<Rigidbody2D> _backgroundsRigidBody = new List<Rigidbody2D>();
    private List<Rigidbody2D> _obstaclesRigidBody = new List<Rigidbody2D>();
    private List<Rigidbody2D> _customersRigidBody = new List<Rigidbody2D>();

    private void Awake()
    {
        var childRbs = this.GetComponentsInChildren<Rigidbody2D>();   

        foreach(var childRb in childRbs)
        {
            if (childRb.gameObject.tag == "Floor")
            {
                _floorsRigidBody.Add(childRb);
            }
            else if (childRb.gameObject.tag == "Background")
            {
                _backgroundsRigidBody.Add(childRb);
            }
        }

        this.OnIncreaseDifficulty += IncreaseRigidbodiesVelocity;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(var floorRigidBody in _floorsRigidBody)
        {
            floorRigidBody.velocity = new Vector2(GameController.Instance.FloorScrollSpeed, 0);
        }

        foreach (var backgroundRigidBody in _backgroundsRigidBody)
        {
            backgroundRigidBody.velocity = new Vector2(GameController.Instance.BackgroundScrollSpeed, 0);
        }
    }

    void IncreaseRigidbodiesVelocity()
    {
        foreach (var floorRigidBody in _floorsRigidBody)
        {
            floorRigidBody.velocity = new Vector2(GameController.Instance.FloorScrollSpeed, 0);
        }

        foreach (var backgroundRigidBody in _backgroundsRigidBody)
        {
            backgroundRigidBody.velocity = new Vector2(GameController.Instance.BackgroundScrollSpeed, 0);
        }

        foreach (var obstacleRigidBody in _obstaclesRigidBody)
        {
            obstacleRigidBody.velocity = new Vector2(GameController.Instance.FloorScrollSpeed, 0);
        }

        foreach (var customerRigidBody in _customersRigidBody)
        {
            customerRigidBody.velocity = new Vector2(GameController.Instance.BackgroundScrollSpeed, 0);
        }

        _obstaclesRigidBody.Clear();
        _customersRigidBody.Clear();
    }

    public void AddObstacleObject(GameObject scrollable)
    {
        _obstaclesRigidBody.Add(scrollable.GetComponent<Rigidbody2D>());
    }

    public void AddCustomerObject(GameObject scrollable)
    {
        _customersRigidBody.Add(scrollable.GetComponent<Rigidbody2D>());
    }

    public void StopScrolling()
    {
        this.StopAllCoroutines();
        foreach (var floorRigidBody in _floorsRigidBody)
        {
            floorRigidBody.velocity = Vector2.zero;
        }

        foreach (var backgroundRigidBody in _backgroundsRigidBody)
        {
            backgroundRigidBody.velocity = Vector2.zero;
        }

        foreach (var obstacleRigidBody in _obstaclesRigidBody)
        {
            obstacleRigidBody.velocity = Vector2.zero;
        }

        foreach (var customerRigidBody in _customersRigidBody)
        {
            customerRigidBody.velocity = Vector2.zero;
        }
    }
}
