using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemRuneCrossbow : ShopItem
{
    [Header("Specific")]
    public Texture runeTexture;
    public Material runeMaterial;
    public Renderer crossbowRenderer;
    public Renderer triggerRenderer;
    public Renderer leverRenderer;
    public Renderer scopeRenderer;
    
    public override void ItemPurchased()
    {
        upgraded = true;
        //crossbowRenderer.material.SetTexture("_MainTex", runeTexture);
        crossbowRenderer.material = runeMaterial;
        triggerRenderer.material = runeMaterial;
        leverRenderer.material = runeMaterial;
        scopeRenderer.material = runeMaterial;
    }
}
