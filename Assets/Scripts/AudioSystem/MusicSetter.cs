using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSetter : MonoBehaviour
{

    public AudioInfo audioInfo;
    public float newVolume = -1;
    void Start()
    {
        AudioManager.instance.TransMusicTo(audioInfo, newVolume);
    }

    void Update()
    {
        
    }
}
