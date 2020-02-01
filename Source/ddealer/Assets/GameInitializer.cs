using UnityEngine;

/// <summary>
/// Script to attach to the camera in Splash scene when it'll be ready
/// At the moment it's used in MainMenuManager's Awake()
/// </summary>
public class GameInitializer : MonoBehaviour
{

    private void Awake()
    {
        if (PrefsHelper.IsFirstLaunch)
        {
            InitializePlayerValues();
            InitializeShopItems();
            PrefsHelper.IsFirstLaunch = false;
        }
    }

    /// <summary>
    /// Initialize values realted to the player like Money
    /// </summary>
    public void InitializePlayerValues()
    {
        PrefsHelper.Money = 0;
    }

    /// <summary>
    /// Creates shop's objects
    /// </summary>
    public void InitializeShopItems()
    {
        ShopItemModel weed = new ShopItemModel
        {
            Name = "Weed",
            Bought = true,
            Cost = 100,
            Description = "The first thing you'll sell",
            Image = "",
            Category = ShopItemModel.ItemCategory.Drug,
            Equipped = true
        };
        ShopItemModel backpack = new ShopItemModel
        {
            Name = "Starter Backpack",
            Bought = true,
            Cost = 100,
            Description = "Your first backpack which contains 20 items",
            Image = "",
            Equipped = true
        };
        ShopItemModel starterSkin = new ShopItemModel
        {
            Name = "Starter Skin",
            Bought = true,
            Cost = 100,
            Description = "You",
            Image = "",
            Equipped = true
        };

        PrefsHelper.ShopItems = new System.Collections.Generic.List<ShopItemModel>
        {
            weed, backpack, starterSkin
        };
    }
}
