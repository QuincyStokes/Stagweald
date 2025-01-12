using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemRuneCrossbow : ShopItem
{
    [Header("Specific")]
    public Texture runeTexture;
    public Material runeMaterial;
    public Renderer crossbowRenderer;
    
    public override void ItemPurchased()
    {
        upgraded = true;
        //crossbowRenderer.material.SetTexture("_MainTex", runeTexture);
        crossbowRenderer.material = runeMaterial;
    }
}
