using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneUpgrade : Upgrade
{
    [Header("Rune Crossbow Texture")]
    public Texture runeTexture;
    public Renderer crossbowRenderer;

    [Header("Crossbow")]
    public GameObject crossbow;
    public void Upgrade()
    {
      
         print("Upgrading!");
        if(InventoryManager.Instance.SubtractGold(price))
        {
            print("Bought for " + price + "gold.");
            InventoryManager.Instance.UpdateGold();
            upgraded = true;
            crossbowRenderer.material.SetTexture("_MainTex", runeTexture);
        }
    }
}
