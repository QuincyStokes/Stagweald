using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ShopItemLever : ShopItem
{
    [Header("References")]
    public GameObject trigger;
    public GameObject lever;
    public override void ItemPurchased()
    {
        upgraded = true;
        trigger.SetActive(false);
        lever.SetActive(true);
    }
}
