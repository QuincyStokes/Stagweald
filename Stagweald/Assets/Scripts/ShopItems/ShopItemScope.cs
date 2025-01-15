using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemScope : ShopItem
{
    [Header("Specific")]
    public GameObject scope;

    public Crossbow crossbow;

    public override void ItemPurchased()
    {
        upgraded = true;
        scope.SetActive(true);
        crossbow.hasScope = true;
        //will also need to adjust ADS animation probably

    }


}
