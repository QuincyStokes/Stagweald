using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ButtonFunctions : MonoBehaviour
{
    public TrapperUILowerController trapperUI;
    public AudioClip buttonClick;
    public AudioMixerGroup amg;
    
    public void SetBuyMode()
    {
        trapperUI.buyMode = true;
    }

    public void SetSellmode()
    {
        trapperUI.buyMode = false;
    }

    public void PlayButtonClick()
    {
        AudioManager.Instance.PlayOneShotVariedPitch(buttonClick, 1f, amg, .05f);
    }
}
