using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemBolts : ShopItem
{
    public override void ItemPurchased()
    {
        InventoryManager.Instance.AddBolts(5);
    }
}
