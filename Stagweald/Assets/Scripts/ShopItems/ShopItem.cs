using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopItem : MonoBehaviour
{
    [Header("Properties")]
    public Sprite upgradeIcon;
    [HideInInspector]
    public bool upgraded;
    public string itemName;
    public string description;
    public int price;
    public bool oneTimePurchase;

    [Header("UI References")]
    public Image image;
    public TMP_Text itemNameUI;
    public TMP_Text descriptionUI;

    [Header("Lower Shop")]
    public TrapperUILowerController lowerUI;
    
    void Start()
    {
        image.sprite = upgradeIcon;
        itemNameUI.text = itemName;
        descriptionUI.text = description;
    }

    public abstract void ItemPurchased();

    public void UpdateLowerUI()
    {
        
        lowerUI.currentShopItem = this;
        lowerUI.RefreshLowerUI(true);
    }
}
