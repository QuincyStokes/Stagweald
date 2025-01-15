using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellItemHide : SellItem
{
    public override void ItemSold(int amount)
    {
        InventoryManager.Instance.SubtractHides(amount);
        InventoryManager.Instance.AddGold(amount * sellPrice);
    }
    
}
