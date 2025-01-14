using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;
using Unity.VisualScripting;
using System.Diagnostics;

public class BackgroundMusicController : MonoBehaviour
{

    [Header("Background Music Settings")]
    public AudioClip[] backgroundMusic;
    private bool backgroundMusicPlaying;
    private AudioClip currentBackgroundMusic;
    public AudioMixerGroup bgMixerGroup;
    public float fadeDuration;

    void Start()
    {
        backgroundMusicPlaying = false;
        currentBackgroundMusic = null;
    }


    void Update()
    {
        if(!backgroundMusicPlaying)
        {
            PlayBackgroundMusic();
        }
    }
    //background music
    public void PlayBackgroundMusic()
    {

        if(backgroundMusic.Count() > 0)
        {
            AudioClip bg = backgroundMusic[Random.Range(0, backgroundMusic.Count())];
            if(bg == currentBackgroundMusic)
            {
                PlayBackgroundMusic();
                return;
            }
            else
            {
                backgroundMusicPlaying = true;
                //have a clip we didn't just play
                currentBackgroundMusic = bg;
                print("Playing " + bg.name);
                StartCoroutine(AudioManager.Instance.PlayAndExitFade(bg, 1f, bgMixerGroup, 5f));
                StartCoroutine(WaitForBackgroundMusic(bg.length));
            }
        }
        //can do same thing with ambient noise i think, but should give it a chance to play
    }

    public IEnumerator WaitForBackgroundMusic(float time)
    {
        yield return new WaitForSeconds(time +1);
        backgroundMusicPlaying = false;
    }

    

}
