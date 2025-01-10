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

    [Header("UI")]
    public TMP_Text numDeerHidesUI;
    public TMP_Text numAntlersUI;


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
        UpdateInventoryUI();
    }   

    public void AddItems(int moreHides=0, int moreAntlers=0)
    {
        numDeerHides += moreHides;
        numAntlers+= moreAntlers;
        UpdateInventoryUI();
    }


    void UpdateInventoryUI()
    {
        numDeerHidesUI.text = "Deer Hide: " + numDeerHides;
        numAntlersUI.text = "Antlers: " + numAntlers;
    }
}
