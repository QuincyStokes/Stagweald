using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    //should hide these in the inspector eventually, can expose them for nw

    [Header("Inventory")]
    public int numDeerHides; //1
    public int numAntlers; //2
    public int numMushrooms;
    public int numBolts; //3
    public float gold; //-1

    [Header("Keybinds")]
    public KeyCode inventoryKeyCode;

    [Header("UI")]
    public GameObject inventoryUI;
    public TMP_Text numDeerHidesUI;
    public TMP_Text numAntlersUI;
    public TMP_Text goldAmountUI;
    public TMP_Text numMushroomsUI;
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
        inventoryUI.SetActive(false);
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
                inventoryUI.SetActive(false);
                inventoryActive = false;
                UpdateGold();
            } else{
                inventoryUI.SetActive(true);
                inventoryActive = true;
                UpdateGold();
            }
        }
    }

    public void AddItems(int moreHides=0, int moreAntlers=0, int moreMushrooms=0)
    {
        numDeerHides += moreHides;
        numAntlers+= moreAntlers;
        numMushrooms += moreMushrooms;
        UpdateInventoryUI();
    }

    public bool SubtractHides(int amount)
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
        print("adding gold " + amount);
        gold += amount;
        UpdateGold();
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
        if(goldAmountUI)
        {
            goldAmountUI.text = gold.ToString();
        }
        
    }

    public void UpdateInventoryUI()
    {
        numDeerHidesUI.text =  numDeerHides.ToString();
        numAntlersUI.text =  numAntlers.ToString();
        numMushroomsUI.text =  numMushrooms.ToString();
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

    public bool SubtractMushrooms(int amount)
    {
        if(amount > numMushrooms)
        {
            return false;
        }
        else
        {
            numMushrooms -= amount;
            UpdateInventoryUI();
            return true;
        }
    }
}
