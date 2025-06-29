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


        if (soundEffectsclips != null)
        {
            soundEffects = new AudioSource[soundEffectsclips.Length];
            for (int i = 0; i < soundEffectsclips.Length; i++)
            {
                GameObject sfxObj = new GameObject($"SFX_{i}");
                sfxObj.transform.SetParent(transform); // 设为子物体
                soundEffects[i] = sfxObj.AddComponent<AudioSource>();
                soundEffects[i].playOnAwake = false;
            }
        }
        InitializeAudioSource();
    }

    void Start()
    {
       
            //Play(0);
            //PlaySoundEffects(2);
            //SetSoundEffectVolume(2, 0.01f);
            //PlaySoundEffects(0);
       
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
        Debug.Log($"play in {clipIndex}");
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


    public void SetSoundEffectVolume(int clipIndex, float newVolume)
    {
        clipIndex = Mathf.Clamp(clipIndex, 0, soundEffectsclips.Length - 1);

        newVolume = Mathf.Clamp01(newVolume);
        soundEffects[clipIndex].volume = newVolume;
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
    public void Update()
    {
        SetLoop(loop);
        SetVolume(volume);
        SetPitch(pitch);
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