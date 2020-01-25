using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get => _instance; }
    private static UIManager _instance;

    [SerializeField]
    private TextMeshProUGUI _backpackText;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private GameObject _deathMenu;

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

    public void SetBackpackText(int quantity)
    {
        _backpackText.text = quantity.ToString();
    }

    public void SetScoreText(int quantity)
    {
        _scoreText.text = quantity.ToString();
    }

    public void ShowDeathMenu()
    {
        _deathMenu.SetActive(true);
    }
}
