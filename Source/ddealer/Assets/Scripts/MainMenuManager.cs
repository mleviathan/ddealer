using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private const float BLACK_PANELS_FADE_TIME = 1f;
    private const float MENU_SCALE_TIME = 1.5f;

    [SerializeField]
    private GameObject _shopMenu;
    [SerializeField]
    private GameObject _shopPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OpenShop()
    {
        _shopMenu.SetActive(true);
        _shopPanel.SetActive(true);
        iTween.FadeTo(_shopPanel, 250f, BLACK_PANELS_FADE_TIME);
        iTween.ScaleTo(_shopMenu, new Vector3(1f, 1f, 1f), MENU_SCALE_TIME);
    }

    public void CloseShop()
    {
        iTween.FadeTo(_shopPanel, 0f, BLACK_PANELS_FADE_TIME);
        //iTween.ScaleTo(_shopMenu, Vector3.zero, MENU_SCALE_TIME);
        iTween.ScaleTo(_shopMenu, iTween.Hash("scale", Vector3.zero, "time", MENU_SCALE_TIME, "oncomplete", "DeactivateShop", "oncompletetarget", this.gameObject));
    }

    public void DeactivateShop()
    {
        _shopPanel.SetActive(false);
        _shopMenu.SetActive(false);
    }
}
