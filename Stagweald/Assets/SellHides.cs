using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SellHides : SellItem
{
    //this clickable even if they have no hides, should probably just grey out the button or something 

    
    void OnEnable()
    {
        UpdateInventoryAmount();
        sellOneButton.GetComponentInChildren<TMP_Text>().text = "Sell One (" +sellPrice.ToString() + "g)";
    }

    public void SellOneHide()
    {
        if(InventoryManager.Instance.SubrtactHides(1))
        {
            InventoryManager.Instance.AddGold(sellPrice);
            UpdateInventoryAmount();
        }
        else
        {
            print("Not enough hides");
        }
    }

    //will do nothing if the player has no hides, thats fine for now
    public void SellAllHides()
    {
        InventoryManager.Instance.AddGold(InventoryManager.Instance.numDeerHides * sellPrice);
        InventoryManager.Instance.SubrtactHides(InventoryManager.Instance.numDeerHides);
        UpdateInventoryAmount();
    }

    void UpdateInventoryAmount()
    {
        if(inventoryAmount != null && InventoryManager.Instance != null)
        {
            inventoryAmount.text = "Inventory:\n" + InventoryManager.Instance.numDeerHides.ToString();
            InventoryManager.Instance.UpdateGold();
            sellAllButton.GetComponentInChildren<TMP_Text>().text = "Sell All (" + (sellPrice * InventoryManager.Instance.numDeerHides).ToString()  + "g)";
        }
        if(InventoryManager.Instance.numDeerHides == 0)
        {
            sellOneButton.interactable = false;
            sellAllButton.interactable = false;
        } 
        else
        {
            sellOneButton.interactable = true;
            sellAllButton.interactable = true;
        }
        
    }
}
