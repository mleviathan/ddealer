using System.Collections;
using System.Collections.Generic;

public class ShopItemModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public ItemCategory Category { get; set; }
    public ItemStatus  Status { get; set; }
    public int? Doses { get; set; }
    public int? SellingPrice { get; set; }

    public enum ItemCategory
    {
        Drug = 0,
        Skin,
        Backpack
    }

    public enum ItemStatus
    {
        None = 0,
        Bought,
        Equipped
    }
}
