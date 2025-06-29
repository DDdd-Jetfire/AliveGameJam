using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoReceiver : MonoBehaviour
{

    public string onVideoStartName;
    public string onVideoEndName;
    public UnityEditor.SceneAsset nextScene;

    public AudioPlayer ap;

    void Start()
    {
        GlobalEventManager.instance.RegisterEvent(onVideoStartName, OnVideoStart);
        GlobalEventManager.instance.RegisterEvent(onVideoEndName, OnVideoFinnished);
    }

    void Update()
    {

    }

    private void OnVideoStart()
    {
        ap.PlaySoundEffects(0);
    }

    private void OnVideoFinnished()
    {
        GameManager.instance.GoToNextScene(nextScene.name);
    }


    private void OnDestroy()
    {
        //GlobalEventManager.instance.UnregisterEvent(receivedEventName, CheckAll);
        GlobalEventManager.instance.UnregisterEvent(onVideoStartName, OnVideoStart);
        GlobalEventManager.instance.RegisterEvent(onVideoEndName, OnVideoFinnished);
    }
}
