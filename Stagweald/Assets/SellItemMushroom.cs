using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellItemMushroom : SellItem
{
    public override void ItemSold(int amount)
    {
        InventoryManager.Instance.SubtractMushrooms(amount);
        InventoryManager.Instance.AddGold(amount * sellPrice);
    }
}
