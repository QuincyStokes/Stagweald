using System.Collections;
using System.Collections.Generic;
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

    public void PlayOneShot(AudioClip clip, float volume, AudioMixerGroup mixerGroup)
    {
        AudioSource source = GetAvailableSource();
        source.clip = clip;
        source.outputAudioMixerGroup = mixerGroup;
        source.volume = volume;
        source.spatialBlend = 0f; // 2D by default; adjust as needed
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
}

