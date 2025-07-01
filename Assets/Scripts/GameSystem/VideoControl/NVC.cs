using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class NVC : MonoBehaviour
{
    public NVideoPlayer nvp;
    public VideoPlayer vp;

    private bool isPrepareComplete = false;
    public bool isPlaying = false;  

    public bool autoPlayAfterLoad = false;
    public bool isVideoLoop = true;
    public float playSpeed = 1f;

    public string onTriggerEventName = "null";
    public string videoPlayEventName = "null";
    public string videoFinishEventName = "null";

    void Start()
    {
        nvp.Loop = isVideoLoop;
        EventSetting();
    }

    private void EventSetting()
    {
        if (onTriggerEventName != "null")
        {
            GlobalEventManager.instance.RegisterEvent(onTriggerEventName, PlayVideo);
        }

        vp.started += OnVideoStart;
        vp.loopPointReached += OnVideoFinish;
        // ׼����������Ƶ
        //vp.Prepare();
        nvp.Init();
        vp.prepareCompleted += OnVideoPrepared;
    }

    //public Action videoStartAction;
    private void OnVideoStart(VideoPlayer source)
    {
        //videoStartAction?.Invoke();
        if (videoPlayEventName != "null")
        {
            GlobalEventManager.instance.TriggerEvent(videoPlayEventName);
        }
    }

    //public Action videoFinishAction;
    private void OnVideoFinish(VideoPlayer source)
    {
        //videoFinishAction?.Invoke();
        Debug.Log("videoFinishEventName");
        if (videoFinishEventName != "null")
        {
            Debug.Log($"trigger {videoFinishEventName}");
            GlobalEventManager.instance.TriggerEvent(videoFinishEventName);
        }
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        Debug.Log($"{gameObject.name} Prepare Complete!");
        isPrepareComplete = true;
        // �����һ֡
        StartCoroutine(HandleFirstFrame());
        //vp.Play();
    }

    IEnumerator HandleFirstFrame()
    {

        // �ȴ���Ⱦ��ʼ
        // �ȴ�һ֡ȷ����Ⱦ
        yield return null;
        nvp.Play();
        nvp.Pause();
        vp.StepForward();
        nvp.Pause();
        //while (vp.frame < 1)
        //{
        //    Debug.Log("waiting");
        //}
        SetVideoSpeed(playSpeed);

        Debug.Log("waiting complete");
        // �Զ�����
        if (autoPlayAfterLoad)
        {
            yield return new WaitForSeconds(0.1f); // �����ӳ�
            PlayVideo();
        }
        else
        {
            nvp.Pause();
        }
    }

    public void PlayVideo()
    {
        if (!isPrepareComplete) return;
        nvp.Play();
        //vp.Play();
        isPlaying = true;
    }
    public void PauseVideo()
    {
        if (!isPrepareComplete) return;
        //vp.Pause();
        nvp.Pause();
        isPlaying = false;
    }
    public void StopVideo()
    {
        nvp.Stop();
        //vp.Stop();
        isPlaying = false;
    }
    public void SetVideoSpeed(float speed)
    {
        if (vp.canSetPlaybackSpeed)
        {
            //Debug.Log($"{gameObject.name} set play speed to {speed}");
            vp.playbackSpeed = speed;
        }
    }

    private void OnDestroy()
    {
        // ע���¼�
        GlobalEventManager.instance.UnregisterEvent(onTriggerEventName, PlayVideo);
        vp.started -= OnVideoStart;
        vp.loopPointReached -= OnVideoFinish;
        vp.prepareCompleted -= OnVideoPrepared;
    }
}