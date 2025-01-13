using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public TrapperUILowerController trapperUI;
    
    public void SetBuyMode()
    {
        trapperUI.buyMode = true;
    }

    public void SetSellmode()
    {
        trapperUI.buyMode = false;
    }
}
