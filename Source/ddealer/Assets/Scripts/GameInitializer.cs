using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to attach to the camera in Splash scene when it'll be ready
/// At the moment it's used in MainMenuManager's Awake()
/// Remember to add MonoBehaviour as father and uncomment Awake()
/// </summary>
public class GameInitializer {

    /*private void Awake()
    {
        if (PrefsHelper.IsFirstLaunch)
        {
            InitializePlayerValues();
            InitializeShopItems();
            PrefsHelper.IsFirstLaunch = false;
        }
    }*/

    /// <summary>
    /// Initialize values realted to the player like Money
    /// </summary>
    public void InitializePlayerValues()
    {
        GameController.Instance.Money = 100;
    }

    /// <summary>
    /// Creates shop's objects
    /// </summary>
    public void InitializeShopItems()
    {
        List<ShopItemModel> shopItems = new List<ShopItemModel>();

        shopItems.AddRange(CreateDrugs());
        shopItems.AddRange(CreateBackpacks());
        shopItems.AddRange(CreateSkins());

        PrefsHelper.ShopItems = shopItems;
    }

    private List<ShopItemModel> CreateDrugs()
    {
        List<ShopItemModel> drugs = new List<ShopItemModel>();
        ShopItemModel weed = new ShopItemModel
        {
            Id = 0,
            Name = "Weed",
            Cost = 70,
            Description = "The first thing you'll sell",
            Image = "",
            Category = ShopItemModel.ItemCategory.Drug,
            Status = ShopItemModel.ItemStatus.Bought,
            Doses = 10,
            SellingPrice = 10,
            OwnedDoses = 0
        };
        drugs.Add(weed);
        ShopItemModel coke = new ShopItemModel
        {
            Id = 1,
            Name = "Coke",
            Cost = 300,
            Description = "The most used",
            Image = "",
            Category = ShopItemModel.ItemCategory.Drug,
            Status = ShopItemModel.ItemStatus.None,
            Doses = 10,
            SellingPrice = 50,
            OwnedDoses = 0
        };
        drugs.Add(coke);
        return drugs;
    }

    private List<ShopItemModel> CreateBackpacks()
    {
        List<ShopItemModel> backpacks = new List<ShopItemModel>();
        ShopItemModel starter = new ShopItemModel
        {
            Id = 3,
            Name = "Starter Backpack",
            Cost = 100,
            Description = "Your first backpack which contains 20 items",
            Image = "",
            Status = ShopItemModel.ItemStatus.Equipped,
            Category = ShopItemModel.ItemCategory.Backpack,
            Load = 20
        };
        backpacks.Add(starter);
        return backpacks;
    }

    private List<ShopItemModel> CreateSkins()
    {
        List<ShopItemModel> skins = new List<ShopItemModel>();
        ShopItemModel starterSkin = new ShopItemModel
        {
            Id = 4,
            Name = "Starter Skin",
            Cost = 100,
            Description = "You",
            Image = "",
            Status = ShopItemModel.ItemStatus.Equipped,
            Category = ShopItemModel.ItemCategory.Skin
        };
        skins.Add(starterSkin);
        return skins;
    }
}
