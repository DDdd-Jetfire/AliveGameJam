using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    public List<AudioInfo> audioInfoList = new List<AudioInfo>();
    private AudioPlayer ap;
    private string currentMusicName = "null";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ap = gameObject.GetComponent<AudioPlayer>();
        if (ap == null)
        {
            Debug.Log("no audioPlayer connection!");
        }
    }

    public void TransMusicTo(AudioInfo ai,float newVolume = -1)
    {
        if (newVolume != -1)
        {
            ap.SetVolume(newVolume);
        }
        else
        {
            ap.SetVolume(ai.volume);
        }
        if (currentMusicName == ai.audioClip.name)
        {
            return;
        }
        for(int i = 0; i < audioInfoList.Count; i++)
        {
            if (audioInfoList[i].audioClip.name == ai.audioClip.name)
            {
                ap.AddClip(audioInfoList[i].audioClip);
                ap.Play(i);
                return;
            }
        }
        Debug.LogError("no fit music!");
    }
}
