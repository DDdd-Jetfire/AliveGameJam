using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="AudioInfo",menuName ="System/AudioInfo")]
public class AudioInfo : ScriptableObject
{
    //public string audioName = "null";
    public AudioClip audioClip;
    public float volume = 1;
}
