using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Helper
{
    public static class ShopItemExtensioneMethods
    {
        public static List<ShopItemModel> OrderByCategory(this List<ShopItemModel> shopItems)
        {
            return shopItems.OrderBy((item) => item.Category).ToList();
        }

        public static List<ShopItemModel> FilterByCategory(this List<ShopItemModel> shopItems, ShopItemModel.ItemCategory category)
        {
            return shopItems.Where((item) => item.Category == category).ToList();
        }

        public static List<ShopItemModel> FilterByStatus(this List<ShopItemModel> shopItems, ShopItemModel.ItemStatus status)
        {
            return shopItems.Where((item) => item.Status == status).ToList();
        }
    }
}
