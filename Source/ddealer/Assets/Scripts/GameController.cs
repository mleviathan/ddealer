﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool GameOver { get; set; }
    public static GameController Instance { get => _instance; }
    private static GameController _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        } 
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}