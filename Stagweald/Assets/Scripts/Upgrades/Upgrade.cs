using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [Header("Properties")]
    public Sprite upgradeIcon;
    public int price;
    [HideInInspector]
    public bool upgraded;

    [Header("UI References")]
    public Image image;
    public TMP_Text priceText;
    public Button buyButton;
    
    void Start()
    {
        image.sprite = upgradeIcon;
        priceText.text = price + "g";
    }
}
