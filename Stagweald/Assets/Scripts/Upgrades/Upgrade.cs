using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [Header("Properties")]
    public Sprite upgradeIcon;
    [HideInInspector]
    public bool upgraded;
    public string itemName;
    public string description;
    public int price;

    [Header("UI References")]
    public Image image;
    public TMP_Text itemNameUI;
    public TMP_Text descriptionUI;
    
    void Start()
    {
        image.sprite = upgradeIcon;
        itemNameUI.text = itemName;
        descriptionUI.text = description;
    }
}
