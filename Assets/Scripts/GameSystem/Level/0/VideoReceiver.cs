using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoReceiver : MonoBehaviour
{

    public string onVideoEndName;
    public UnityEditor.SceneAsset nextScene;

    void Start()
    {
        GlobalEventManager.instance.RegisterEvent(onVideoEndName, OnVideoFinnished);
    }

    void Update()
    {

    }

    private void OnVideoFinnished()
    {
        GameManager.instance.GoToNextScene(nextScene.name);
    }


    private void OnDestroy()
    {
        //GlobalEventManager.instance.UnregisterEvent(receivedEventName, CheckAll);
        GlobalEventManager.instance.UnregisterEvent(onVideoEndName, OnVideoFinnished);
    }
}
