using System;
using System.Collections;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrapperUILowerController : MonoBehaviour
{
    [Header("UI References")]
    public Image image;
    public TMP_Text itemName;
    public TMP_Text buyAmount;
    public Slider amountSlider;
    public TMP_Text price;
    public Button button;

    [Header("Properties")]
    public int currentItemPrice;
    private float currentBuyAmount;
    public ShopItem currentShopItem;

    public void amountSliderChanged()
    {
        buyAmount.text = "BUY x" + amountSlider.value.ToString();
        currentBuyAmount = amountSlider.value;
        if(amountSlider.value * currentItemPrice == 0)
        {
            price.text = currentItemPrice.ToString();
        }
        else
        {
            price.text = (amountSlider.value * currentItemPrice).ToString();
        }
    }


    public void RefreshLowerUI()
    {
        image.sprite = currentShopItem.upgradeIcon; //ICON
        itemName.text = currentShopItem.itemName; //NAME
        currentItemPrice = currentShopItem.price; //inner PRUCE
        price.text = currentItemPrice.ToString(); //PRICE


        //slider
        if(InventoryManager.Instance.gold >= currentItemPrice)
        {
            amountSlider.minValue = 1;
            button.interactable = true;
            //if they can afford it, and it is not a one time purchase,
            // update the max value of the slider 
            if(currentShopItem.oneTimePurchase == true)
            {
                amountSlider.maxValue = 1;
            }
            else //can buy multiple of this item
            {
                amountSlider.maxValue = (int)(InventoryManager.Instance.gold / currentItemPrice);
            }
            
        }
        else{//set to 0, cannot afford
            amountSlider.minValue = 0;
            amountSlider.maxValue = 0;
            button.interactable = false;
        }

        if(currentShopItem.upgraded == true && currentShopItem.oneTimePurchase == true)
        {
            buyAmount.text = "SOLD";
            button.interactable = false;
        } 
        else
        {
            buyAmount.text = "BUY x" + amountSlider.value.ToString();
        }
    }

    public void Buy()
    {
        if(!currentShopItem.upgraded)
        {
            if(InventoryManager.Instance.SubtractGold(amountSlider.value * currentItemPrice))
            {
                for (int i = 0; i < amountSlider.value; i++)
                {
                    currentShopItem.ItemPurchased();
                }
                
            }
            else{
                print("Not enough gold.");
            }
            
            
        }
        RefreshLowerUI();
        
    }

}
