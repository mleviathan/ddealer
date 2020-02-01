using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PrefsHelper
{
    private const string SHOP_ITEMS_KEY = "shopItems";
    private const string FIRST_LAUNCH = "firstLaunch";
    private const string MONEY = "money";

    public static List<ShopItemModel> ShopItems
    {
        get
        {
            if (PlayerPrefs.HasKey(SHOP_ITEMS_KEY))
                return JsonConvert.DeserializeObject<List<ShopItemModel>>(PlayerPrefs.GetString(SHOP_ITEMS_KEY));

            return null;
        }
        set
        {
            if (value != null)
            {
                PlayerPrefs.SetString(SHOP_ITEMS_KEY, JsonConvert.SerializeObject(value));
                PlayerPrefs.Save();
            }
        }
    }

    public static bool IsFirstLaunch
    {
        get
        {
            if (PlayerPrefs.HasKey(FIRST_LAUNCH))
                return PlayerPrefs.GetInt(FIRST_LAUNCH) == 0 ? false : true;

            return true;
        }
        set
        {
            PlayerPrefs.SetInt(FIRST_LAUNCH, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static int Money
    {
        get
        {
            if (PlayerPrefs.HasKey(MONEY))
                return PlayerPrefs.GetInt(MONEY);

            return 0;
        }
        set
        {
            PlayerPrefs.SetInt(MONEY, value);
            PlayerPrefs.Save();
        }
    }

}
