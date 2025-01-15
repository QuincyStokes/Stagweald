using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CameraSFX : MonoBehaviour
{
    [Header("Footstep Sounds")] 
    public AudioClip grass_footstep;
    [Header("Audio Mixer Group")]
    public AudioMixerGroup amg;
    public void PlayFootstep()
    {
        AudioManager.Instance.PlayOneShotVariedPitch(grass_footstep, .8f, amg, .12f);
    }
}
