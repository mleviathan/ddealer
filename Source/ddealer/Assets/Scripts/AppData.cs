using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AppData : MonoBehaviour
{
    public static AppData Instance { get => _instance; }
    private static AppData _instance;

    private int _money;
    private int _backpackLoad;

    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            OnMoneySet(value);
        }
    }
    public int BackpackLoad
    {
        get => _backpackLoad;
        set
        {
            _backpackLoad = value;
            OnLoadSet(value);
        }
    }
    public List<ShopItemModel> ShopItems { get; set; }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        Money = PrefsHelper.Money;
        BackpackLoad = PrefsHelper.ShopItems.First((bp) => bp.Category == ShopItemModel.ItemCategory.Backpack
                                                    && bp.Status == ShopItemModel.ItemStatus.Equipped).Load;
        ShopItems = PrefsHelper.ShopItems;
    }

    public void OnMoneySet(int newValue)
    {
        var mainMenuMgr = FindObjectOfType<MainMenuManager>();
        if (mainMenuMgr != null)
            mainMenuMgr.SetMoneyText(newValue);

        PrefsHelper.Money = newValue;
    }

    public void OnLoadSet(int newValue)
    {
        var mainMenuMgr = FindObjectOfType<MainMenuManager>();
        if (mainMenuMgr != null)
            mainMenuMgr.SetLoadText(newValue);
    }

}
