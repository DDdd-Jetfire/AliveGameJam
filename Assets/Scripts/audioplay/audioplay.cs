using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioClip[] soundEffectsclips;
    [SerializeField] private bool playOnAwake = true;
    [SerializeField] private bool loop = false;
    [Range(0f, 1f)] [SerializeField] private float volume = 1f;
    [Range(0.1f, 3f)] [SerializeField] private float pitch = 1f;
    
    
    private AudioSource[] soundEffects;

    private AudioSource audioSource;
    private int currentClipIndex = 0;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        soundEffects = new AudioSource[soundEffectsclips.Length];
        InitializeAudioSource();
    }

    void Start()
    {
        if (playOnAwake && clips.Length > 0)
        {
            Play(1);
        }
    }

    private void InitializeAudioSource()
    {
        audioSource.playOnAwake = false;
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
    }

    public void PlaySoundEffects(int clipIndex)
    {
        if (soundEffectsclips == null || soundEffectsclips.Length == 0)
        {
            Debug.LogWarning("No audio clips assigned!");
            return;
        }

        clipIndex = Mathf.Clamp(clipIndex, 0, soundEffectsclips.Length - 1);
        soundEffects[clipIndex].clip = soundEffectsclips[clipIndex];
        soundEffects[clipIndex].Play();
    

    }
    public void CancelaudioSource()
    {
   
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.time = 0f;
            audioSource.clip = null;
        }
    }

    public void CancelAllSoundEffects()
    {
        foreach (AudioSource source in soundEffects)
        {
 
            source.Stop();       
            source.time = 0f;    
            source.clip = null;  
            
        }
    }

    public void Play(int index = 0)
    {
        if (clips == null || clips.Length == 0)
        {
            Debug.LogWarning("No audio clips assigned!");
            return;
        }

        index = Mathf.Clamp(index, 0, clips.Length - 1);
        audioSource.clip = clips[index];
        audioSource.Play();
        currentClipIndex = index;
    }

  
    public void PlayRandom()
    {
        if (clips.Length == 0) return;
        int randomIndex = Random.Range(0, clips.Length);
        Play(randomIndex);
    }




    public void Stop()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void Pause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

  
    public void UnPause()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        audioSource.volume = volume;
    }

    public void SetPitch(float newPitch)
    {
        pitch = Mathf.Clamp(newPitch, 0.1f, 3f);
        audioSource.pitch = pitch;
    }

    public void SetLoop(bool shouldLoop)
    {
        loop = shouldLoop;
        audioSource.loop = loop;
    }

    public void PlayNext()
    {
        currentClipIndex = (currentClipIndex + 1) % clips.Length;
        Play(currentClipIndex);
    }

    public void PlayPrevious()
    {
        currentClipIndex = (currentClipIndex - 1 + clips.Length) % clips.Length;
        Play(currentClipIndex);
    }

    public void AddClip(AudioClip newClip)
    {
        List<AudioClip> tempList = new List<AudioClip>();
        if (clips != null) tempList.AddRange(clips);
        tempList.Add(newClip);
        clips = tempList.ToArray();
    }


    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}