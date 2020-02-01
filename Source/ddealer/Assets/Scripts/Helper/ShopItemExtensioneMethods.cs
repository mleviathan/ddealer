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
    }
}
