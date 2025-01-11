using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeUpgrade : Upgrade
{
    public void Upgrade()
    {
        //this is where we will add the scope to the model.
        print("Upgrading!");
        if(InventoryManager.Instance.SubtractGold(price))
        {
            print("Bought for " + price + "gold.");
            InventoryManager.Instance.UpdateGold();
            upgraded = true;
            buyButton.interactable = false;
        }
        
        
    }


}
