using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLevel0 : InteractBase
{
    public string onVideoStartName;
    public string onVideoEndName;
    public string nextScene;

    public AudioPlayer ap;
    public bool isClick = false;

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
        if (isClick)
        {
            ap.PlaySoundEffects(0);
            InteractManager.instance.SetDisable();
        }
    }

    private void OnVideoFinnished()
    {
        GameManager.instance.GoToNextScene(nextScene);
    }


    private void OnDestroy()
    {
        //GlobalEventManager.instance.UnregisterEvent(receivedEventName, CheckAll);
        GlobalEventManager.instance.UnregisterEvent(onVideoStartName, OnVideoStart);
        GlobalEventManager.instance.RegisterEvent(onVideoEndName, OnVideoFinnished);
    }

    public override void Click()
    {
        isClick = true;
        base.Click();
    }
}
