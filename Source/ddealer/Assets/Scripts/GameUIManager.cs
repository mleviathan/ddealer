using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get => _instance; }
    private static GameUIManager _instance;

    [SerializeField]
    private TextMeshProUGUI _backpackText;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private GameObject _deathMenu;
    [SerializeField]
    private Image _deathMenuPanel;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);

        _deathMenuPanel.gameObject.SetActive(false);
        _deathMenuPanel.DOFade(0f, .1f);
        _deathMenu.SetActive(false);
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
        _deathMenuPanel.gameObject.SetActive(true); 
        _deathMenuPanel.DOFade(0.8f, 1f);
        _deathMenu.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
