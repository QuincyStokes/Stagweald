using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SellItem : MonoBehaviour
{
    [Header("Properties")]
    public Sprite itemIcon;
    public string itemName;
    public string description;
    public int sellPrice;
    public int itemNumber;

    [Header("UI References")]
    public Image image;
    public TMP_Text item_nameUI;
    public TMP_Text descriptionUI;

    [Header("Lower Shop")]
    public TrapperUILowerController lowerUI;

    void Start()
    {
        image.sprite = itemIcon;
        item_nameUI.text = itemName;
        descriptionUI.text = description;
    }

    public void UpdateLowerUI()
    {
        lowerUI.currentSellItem = this;
        lowerUI.RefreshLowerUI(false);
    }

    public abstract void ItemSold(int amount);
}
