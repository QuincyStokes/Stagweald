using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Audio;
public class AmbientSoundController : MonoBehaviour
{
    [Header("Clips")]
    public AudioClip[] ambientSounds;
    private AudioClip currentSound;
    public AudioMixerGroup amg;
    private bool isAmbientPlaying;

    void Start()
    {
        isAmbientPlaying = false;
    }

    void Update()
    {
        if(!isAmbientPlaying)
        {
            PlayAmbientSound();
        }
    }

    public void PlayAmbientSound()
    {
        if(ambientSounds.Count() > 0)
        {
            isAmbientPlaying = true;
            AudioClip sound = ambientSounds[Random.Range(0, ambientSounds.Count())];
            if(sound == currentSound)
            {
                PlayAmbientSound();
                return;
            }
            else
            {
                AudioManager.Instance.PlayOneShotVariedPitch(sound,1f,amg, .1f);
                StartCoroutine(WaitForAmbientSound(sound.length));
            }
        }
    }

    public IEnumerator WaitForAmbientSound(float time)
    {
        yield return new WaitForSeconds(time*2);
        isAmbientPlaying = false;
    }

    
}
