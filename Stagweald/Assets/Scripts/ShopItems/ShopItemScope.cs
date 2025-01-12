using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemScope : ShopItem
{
    [Header("Specific")]
    public GameObject scope;

    public override void ItemPurchased()
    {
        //this is where we will add the scope to the model.
        print("Upgrading!");
        upgraded = true;
        scope.SetActive(true);
        //will also need to adjust ADS animation probably
    
        
        
    }


}
