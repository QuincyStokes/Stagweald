using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellItemAntler : SellItem
{
   public override void ItemSold(int amount)
    {
        InventoryManager.Instance.SubtractAntlers(amount);
        InventoryManager.Instance.AddGold(amount * sellPrice);
    }
}
