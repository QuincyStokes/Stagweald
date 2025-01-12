using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellAntlers : SellItem
{
    //this clickable even if they have no hides, should probably just grey out the button or something 

    
    void OnEnable()
    {
        UpdateInventoryAmount();
        sellOneButton.GetComponentInChildren<TMP_Text>().text = "Sell One (" +sellPrice.ToString() + "g)";
    }

    public void SellOneAntler()
    {
        if(InventoryManager.Instance.SubtractAntlers(1))
        {
            InventoryManager.Instance.AddGold(sellPrice);
            UpdateInventoryAmount();
        }
        else
        {
            print("Not enough antlers");
        }
    }

    //will do nothing if the player has no hides, thats fine for now
    public void SellAllAntlers()
    {
        InventoryManager.Instance.AddGold(InventoryManager.Instance.numAntlers * sellPrice);
        InventoryManager.Instance.SubtractAntlers(InventoryManager.Instance.numAntlers);
        UpdateInventoryAmount();
    }

    void UpdateInventoryAmount()
    {
        if(inventoryAmount != null && InventoryManager.Instance != null)
        {
            inventoryAmount.text = "Inventory:\n" + InventoryManager.Instance.numAntlers.ToString();
            InventoryManager.Instance.UpdateGold();
            sellAllButton.GetComponentInChildren<TMP_Text>().text = "Sell All (" + (sellPrice * InventoryManager.Instance.numAntlers).ToString()  + "g)";

            if(InventoryManager.Instance.numAntlers == 0)
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
}
