using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Mixer References")]
    [SerializeField] private AudioMixer masterMixer;

    [Header("Audio Source Pool")]
    [SerializeField] private int poolSize = 10;
    private List<AudioSource> audioSourcePool;
    public AudioSource trapperAudioSource;

    

    [Header("Ambient Sound Settings")]
    public AudioClip[] ambientSounds;
    private bool ambientSoundPlaying;
    private AudioClip currentAmbientSound;
    public AudioMixerGroup ambientMixerGroup;

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize pool
            InitializeAudioSourcePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

   

    private void InitializeAudioSourcePool()
    {
        audioSourcePool = new List<AudioSource>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = new GameObject("AudioSource_" + i);
            obj.transform.SetParent(transform);
            AudioSource source = obj.AddComponent<AudioSource>();
            audioSourcePool.Add(source);
        }
    }

    public void PlayOneShot(AudioClip clip, float volume, AudioMixerGroup mixerGroup, AudioSource audioSource = null)
    {
        AudioSource source;
        if(audioSource == null)
        {
            source = GetAvailableSource();
        }
        else{
            source = audioSource;
        }
        
        source.clip = clip;
        source.outputAudioMixerGroup = mixerGroup;
        source.volume = volume;
        source.spatialBlend = 1f; // 2D by default; adjust as needed
        source.PlayOneShot(clip);
    }

    public void PlayOneShotVariedPitch(AudioClip clip, float volume, AudioMixerGroup mixerGroup, float pitchOffset)
    {
        AudioSource source = GetAvailableSource();
        source.clip = clip;
        source.outputAudioMixerGroup = mixerGroup;
        source.volume = volume;
        source.spatialBlend = 0f; // 2D by default; adjust as needed
        source.pitch = Random.Range(1-pitchOffset, 1+pitchOffset);
        source.PlayOneShot(clip);
    }

    private AudioSource GetAvailableSource()
    {
        // Return first available or oldest if none free
        foreach (AudioSource source in audioSourcePool)
        {
            if (!source.isPlaying)
                return source;
        }
        // If all are playing, return the first one
        return audioSourcePool[0];
    }

    // Example function to set exposed parameters
    public void SetMixerGroupVolume(string groupName, float valueInDb)
    {
        masterMixer.SetFloat(groupName, valueInDb);
    }

    public IEnumerator PlayAndExitFade(AudioClip clip, float targetVolume, AudioMixerGroup mixerGroup, float fadeTime)
{
    AudioSource source = GetAvailableSource();
    source.outputAudioMixerGroup = mixerGroup;
    source.spatialBlend = 0f;
    
    // Make sure volume starts at zero if we're fading in
    source.volume = 0f;
    source.clip = clip;
    source.Play();
    print("Fading in with target volume " + targetVolume);
    // ----- FADE IN -----
    float elapsed = 0f;
    while (elapsed < fadeTime)
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / fadeTime);
        source.volume = Mathf.Lerp(0f, targetVolume, t);
        print("volume: " + source.volume);
        yield return null;
    }
    source.volume = targetVolume;

    // Wait for the middle portion of the track
    float sustainTime = clip.length - (fadeTime * 2f);

    if (sustainTime > 0f)
        yield return new WaitForSeconds(sustainTime);
    else
        Debug.LogWarning("Clip length is too short for this fade in/out timing!");

    // ----- FADE OUT -----
    elapsed = 0f;
    float startVol = source.volume;
    print("fading out");
    while (elapsed < fadeTime)
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / fadeTime);
        source.volume = Mathf.Lerp(startVol, 0f, t);
        print("volume: " + source.volume);
        yield return null;
    }
    source.volume = 0f;

    // Stop the AudioSource or let it finish naturally
    source.Stop();
    source.clip = null; 
}




    

}

