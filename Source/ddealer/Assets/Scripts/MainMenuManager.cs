using Assets.Scripts.Helper;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private const float BLACK_PANELS_FADE_TIME = 1f;
    private const float MENU_SCALE_TIME = 1f;

    private List<ShopItemModel> _shopItems = new List<ShopItemModel>();

    [SerializeField]
    private GameObject _shopMenu;
    [SerializeField]
    private Image _shopPanel;
    [SerializeField]
    private GameObject _shopElementPrefab;

    private void Awake()
    {
        // Carico gli item dello shop
        // Rimuovere questo if quando avremo una splash
        if (PrefsHelper.IsFirstLaunch)
        {
            GameInitializer initializer = new GameInitializer();
            initializer.InitializePlayerValues();
            initializer.InitializeShopItems();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _shopItems = PrefsHelper.ShopItems.OrderByCategory();
        CreateShopItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Loads Game scene
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    /// <summary>
    /// Instantiates the list of objects in scene
    /// </summary>
    private void CreateShopItems()
    {
        var shopMenuGrid = _shopMenu.transform.Find("ScrollView - Shop").GetChild(0);

        foreach (var item in _shopItems)
        {
            GameObject instShopElem = Instantiate(_shopElementPrefab, shopMenuGrid);
            var nameTextBox = instShopElem.transform.Find("Container - Texts/Text - Name").GetComponent<TextMeshProUGUI>();
            var descTextBox = instShopElem.transform.Find("Container - Texts/Text - Description").GetComponent<TextMeshProUGUI>();
            var image = instShopElem.transform.Find("Image - ShopElementContainer").GetChild(0).GetComponent<Image>();
            var buyButtonObj = instShopElem.transform.Find("Container - Buttons/Button - Buy").gameObject;
            var buyTextBox = buyButtonObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            var equipButtonObj = instShopElem.transform.Find("Container - Buttons/Button - Equip").gameObject;
            var equipTextBox = equipButtonObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            nameTextBox.text = item.Name;
            descTextBox.text = item.Description;
            //image.sprite = 
            if (item.Bought)
                Destroy(buyButtonObj);
            else
                buyTextBox.text = $"Buy for {item.Cost}";

            if (item.Equipped)
                equipButtonObj.SetActive(false);
        }
    }

    #region Animation callbacks

    public void OpenShop()
    {
        _shopMenu.SetActive(true);
        _shopPanel.gameObject.SetActive(true);
        _shopPanel.DOFade(0.8f, BLACK_PANELS_FADE_TIME);
        _shopMenu.transform.DOScale(new Vector3(1f, 1f, 1f), MENU_SCALE_TIME);
    }

    public void CloseShop()
    {
        _shopPanel.DOFade(0f, BLACK_PANELS_FADE_TIME);
        _shopMenu.transform.DOScale(Vector3.zero, MENU_SCALE_TIME).OnComplete(new TweenCallback(DeactivateShop));
    }

    private void DeactivateShop()
    {
        _shopPanel.gameObject.SetActive(false);
        _shopMenu.SetActive(false);
    }

    #endregion

}
