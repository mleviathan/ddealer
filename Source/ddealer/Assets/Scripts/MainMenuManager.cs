using Assets.Scripts.Helper;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField]
    private TextMeshProUGUI _moneyText;
    [SerializeField]
    private Image _preGamePanel;
    [SerializeField]
    private GameObject _preGameMenu;
    [SerializeField]
    private GameObject _preGameDrugPrefab;
    [SerializeField]
    private TextMeshProUGUI _backpackLoadText;

    private ShopItemModel _selectedDrug;

    private void Awake()
    {
        // Carico gli item dello shop
        // Rimuovere questo if quando avremo una splash
        if (PrefsHelper.IsFirstLaunch)
        {
            GameInitializer initializer = new GameInitializer();
            initializer.InitializePlayerValues();
            initializer.InitializeShopItems();
            //_moneyText.text = AppData.Instance.Money.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _shopItems = PrefsHelper.ShopItems.OrderByCategory();
        CreateShopItems();
    }

    


    /// <summary>
    /// Loads Game scene
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    /// <summary>
    /// Instantiates the list of objects in shop
    /// </summary>
    private void CreateShopItems()
    {
        var shopMenuGrid = _shopMenu.transform.Find("ScrollView - Shop").GetChild(0);

        foreach (var item in _shopItems)
        {
            GameObject instShopElem = Instantiate(_shopElementPrefab, shopMenuGrid);
            TextMeshProUGUI nameTextBox = instShopElem.transform.Find("Container - Texts/Text - Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI descTextBox = instShopElem.transform.Find("Container - Texts/Text - Description").GetComponent<TextMeshProUGUI>();
            Image image = instShopElem.transform.Find("Image - ShopElementContainer").GetChild(0).GetComponent<Image>();
            GameObject buyButtonObj = instShopElem.transform.Find("Container - Buttons/Button - Buy").gameObject;
            TextMeshProUGUI buyTextBox = buyButtonObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            GameObject equipButtonObj = instShopElem.transform.Find("Container - Buttons/Button - Equip").gameObject;
            TextMeshProUGUI equipTextBox = equipButtonObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            nameTextBox.text = item.Name;
            descTextBox.text = item.Description;
            //image.sprite = 
            if (item.Status >= ShopItemModel.ItemStatus.Bought
                && item.Category != ShopItemModel.ItemCategory.Drug)
                Destroy(buyButtonObj);
            else
                buyTextBox.text = $"Buy for {item.Cost}";

            if (item.Status == ShopItemModel.ItemStatus.Equipped ||
                item.Category == ShopItemModel.ItemCategory.Drug)
                equipButtonObj.SetActive(false);

            if (item.Category == ShopItemModel.ItemCategory.Drug)
            {
                var txtQuantity = instShopElem.transform.Find("Image - ShopElementContainer").GetChild(1).GetComponent<TextMeshProUGUI>();
                txtQuantity.text = item.Doses.ToString();
                txtQuantity.gameObject.SetActive(true);
            }

            buyButtonObj.GetComponent<Button>().onClick.AddListener(new UnityAction(() => { BuyItem(item.Id); }));
            equipButtonObj.GetComponent<Button>().onClick.AddListener(new UnityAction(() => { EquipItem(item.Id); }));
        }
    }

    /// <summary>
    /// Instantiates the list of objects in PreGame panel
    /// </summary>
    private void CreateEquipmentItems()
    {
        var equipMenuGrid = _preGameMenu.transform.Find("ScrollView - Equip").GetChild(0);
        var txtBpLoad = _preGameMenu.transform.Find("Text - Helper").GetChild(0).GetComponent<TextMeshProUGUI>();
        txtBpLoad.text = AppData.Instance.BackpackLoad.ToString();

        // Clean from previously created object
        for (int i = 0; i < equipMenuGrid.childCount; i++)
        {
            Destroy(equipMenuGrid.GetChild(i).gameObject);
        }

        var drugs = _shopItems.FilterByCategory(ShopItemModel.ItemCategory.Drug).FilterByStatus(ShopItemModel.ItemStatus.Bought);

        foreach (var drug in drugs)
        {
            GameObject instDrugElem = Instantiate(_preGameDrugPrefab, equipMenuGrid);
            var imageContainer = instDrugElem.transform.Find("Image - EquipElementContainer");
            Image drugImage = imageContainer.Find("Image - EquipElement").gameObject.GetComponent<Image>();
            TextMeshProUGUI txtBoughtDoses = imageContainer.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI txtName = instDrugElem.transform.Find("Text - Name").GetComponent<TextMeshProUGUI>();
            var btnsContainer = instDrugElem.transform.Find("Container - Buttons");
            TextMeshProUGUI txtQuantity = btnsContainer.Find("Input - Quantity/Text Area/Text - Quantity").GetComponent<TextMeshProUGUI>();
            Button btnMinus = btnsContainer.Find("Button - Minus").GetComponent<Button>();
            Button btnPlus = btnsContainer.Find("Button - Plus").GetComponent<Button>();

            //drugImage.sprite = drug.Image;
            txtBoughtDoses.text = drug.OwnedDoses.ToString();
            txtName.text = drug.Name;
            if (AppData.Instance.BackpackLoad < drug.OwnedDoses)
                txtQuantity.text = AppData.Instance.BackpackLoad.ToString();
            else
                txtQuantity.text = drug.OwnedDoses.ToString();

            btnMinus.onClick.AddListener(new UnityAction(() => { DecreaseDrugQuantity(instDrugElem); }));
            btnPlus.onClick.AddListener(new UnityAction(() => { IncreaseDrugQuantity(instDrugElem); }));
        }
    }

    public void DecreaseDrugQuantity(GameObject drugObj)
    {
        var txtQuantity = drugObj.transform.Find("Container - Buttons/Input - Quantity/Text Area/Text - Quantity").GetComponent<TextMeshProUGUI>();

        if (int.TryParse(txtQuantity.text, out int quantity))
        {
            int newQty = quantity -= 5;
            if (newQty >= 0)
                txtQuantity.text = newQty.ToString();
        }
    }

    public void IncreaseDrugQuantity(GameObject drugObj)
    {
        var txtQuantity = drugObj.transform.Find("Container - Buttons/Input - Quantity/Text Area/Text - Quantity").GetComponent<TextMeshProUGUI>();
        if (int.TryParse(txtQuantity.text, out int quantity))
        {
            int newQty = quantity += 5;
            int ownedQty = int.Parse(drugObj.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text);
            
            if (newQty <= AppData.Instance.BackpackLoad && newQty <= ownedQty)
                txtQuantity.text = newQty.ToString();
        }
    }

    public void BuyItem(int id)
    {
        ShopItemModel item = PrefsHelper.ShopItems.First((it) => it.Id == id);
        if (AppData.Instance.Money >= item.Cost)
        {
            var shopItemsCopy = PrefsHelper.ShopItems;
            shopItemsCopy.Remove(item);
            AppData.Instance.Money -= item.Cost;
            item.Status = ShopItemModel.ItemStatus.Bought;

            if (item.Category == ShopItemModel.ItemCategory.Drug)
                item.OwnedDoses += item.Doses;

            shopItemsCopy.Add(item);
            PrefsHelper.ShopItems = shopItemsCopy;
        }
    }

    public void EquipItem(int id)
    {
        var item = _shopItems.First((it) => it.Id == id);
        if (item.Category == ShopItemModel.ItemCategory.Backpack)
        {
            AppData.Instance.BackpackLoad = item.Load;
        }
    }

    #region Animations and callbacks

    public void OpenPreGameMenù()
    {
        CreateEquipmentItems();
        _preGameMenu.SetActive(true);
        _preGamePanel.gameObject.SetActive(true);
        _preGamePanel.DOFade(0.8f, BLACK_PANELS_FADE_TIME);
        _preGameMenu.transform.DOScale(new Vector3(1f, 1f, 1f), MENU_SCALE_TIME);
    }

    public void ClosePreGameMenù()
    {
        _preGamePanel.DOFade(0f, BLACK_PANELS_FADE_TIME);
        _preGameMenu.transform.DOScale(Vector3.zero, MENU_SCALE_TIME).OnComplete(new TweenCallback(DeactivatePreGameMenù));
    }

    private void DeactivatePreGameMenù()
    {
        _preGamePanel.gameObject.SetActive(false);
        _preGameMenu.SetActive(false);
    }

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

    public void SetMoneyText(int value)
    {
        _moneyText.text = value.ToString();
    }

}
