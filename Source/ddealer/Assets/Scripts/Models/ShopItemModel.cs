using System.Collections;
using System.Collections.Generic;

public class ShopItemModel
{
    public string Name { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public bool Bought { get; set; }
    public ItemCategory Category { get; set; }
    public bool Equipped { get; set; }

    public enum ItemCategory
    {
        Drug = 0,
        Skin,
        Backpack
    }
}
