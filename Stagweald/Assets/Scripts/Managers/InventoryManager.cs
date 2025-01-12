using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    //should hide these in the inspector eventually, can expose them for nw

    [Header("Inventory")]
    public int numDeerHides;
    public int numAntlers;
    public int numBolts;
    public float gold;
    

    [Header("Keybinds")]
    public KeyCode inventoryKeyCode;

    [Header("UI")]
    public GameObject inventory;
    public TMP_Text numDeerHidesUI;
    public TMP_Text numAntlersUI;
    public TMP_Text goldAmountUI;
    public TMP_Text numBoltsUI;

    private bool inventoryActive;


    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
        inventory.SetActive(false);
        inventoryActive = false;
        UpdateInventoryUI();
        UpdateGold();
    }   

    void Update()
    {
        if(Input.GetKeyDown(inventoryKeyCode))
        {
            if(inventoryActive)
            {
                inventory.SetActive(false);
                inventoryActive = false;
                UpdateGold();
            } else{
                inventory.SetActive(true);
                inventoryActive = true;
                UpdateGold();
            }
        }
    }

    public void AddItems(int moreHides=0, int moreAntlers=0)
    {
        numDeerHides += moreHides;
        numAntlers+= moreAntlers;
        UpdateInventoryUI();
    }

    public bool SubrtactHides(int amount)
    {
        if(amount > numDeerHides)
        {
            return false;
        }
        else 
        {
            numDeerHides -= amount;
            UpdateInventoryUI();
            return true;
        }
    }

     public bool SubtractAntlers(int amount)
    {
        if(amount > numAntlers)
        {
            return false;
        }
        else 
        {
            numAntlers -= amount;
            UpdateInventoryUI();
            return true;
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }
    
    public bool SubtractGold(float amount)
    {
        if(amount > gold)
        {
            return false;
        } 
        else
        {
            gold -= amount;
            UpdateGold();
            return true;
        }
    }

    public void UpdateGold()
    {
        goldAmountUI.text = gold.ToString();
    }

    public void UpdateInventoryUI()
    {
        numDeerHidesUI.text = "Deer Hide: " + numDeerHides;
        numAntlersUI.text = "Antlers: " + numAntlers;
        numBoltsUI.text = "Bolts: " + numBolts;
    }

    public bool SubtractBolts(int amount)
    {
        if(amount > numBolts)
        {
            return false;
        }
        else 
        {
            numBolts -= amount;
            UpdateInventoryUI();
            return true;
        }
    }

     public void AddBolts(int amount)
    {
        numBolts += amount;
        UpdateInventoryUI();
    }
}
