using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemImprovedCrossbow : ShopItem
{
    [Header("Specific")]
    public Material improvedMaterial;
    public Renderer crossbowRenderer;
    public Renderer triggerRenderer;
    public Renderer leverRenderer;
    public Renderer scopeRenderer;
    public Crossbow crossbow;
    public ShopItemRuneCrossbow shopItemRuneCrossbow;
    
    public override void ItemPurchased()
    {
        upgraded = true;
        //crossbowRenderer.material.SetTexture("_MainTex", runeTexture);
        crossbowRenderer.material = improvedMaterial;
        triggerRenderer.material = improvedMaterial;
        leverRenderer.material = improvedMaterial;
        scopeRenderer.material = improvedMaterial;
        UgpradeCrossbow();
    }

    private void UgpradeCrossbow()
    {
        crossbow.isUpgraded = true;
        shopItemRuneCrossbow.gameObject.SetActive(true);
    }
    
}
