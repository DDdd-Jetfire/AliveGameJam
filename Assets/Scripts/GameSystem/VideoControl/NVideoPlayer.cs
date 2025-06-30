using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(NVideoPlayerModel))]
public class NVideoPlayer : MonoBehaviour
{
    NVideoPlayerModel model;
    /// <summary>
    /// ��ǰ�������Ƿ��ڲ���
    /// </summary>
    public bool IsVideoPlaying
    {
        get { return model.IsVideoPlaying; }
    }
    /// <summary>
    /// ��ȡ��Ƶ����ʱ��
    /// </summary>
    public double PlayTime
    {
        set
        {
            model.PlayTime = value;
            if (!IsVideoPlaying)
            {
                Play();
            }
        }
        get
        {
            return Mathf.Floor((float)model.PlayTime);
        }
    }
    /// <summary>
    /// ȫ��
    /// </summary>
    public bool FullScreen
    {
        set { model.FullScreen(value); }
    }
    /// <summary>
    /// ������Ƶurl
    /// </summary>
    public string VideoUrl
    {
        set { model.VideoSource(value); }
    }
    /// <summary>
    /// ������Ƶ��Դ��VideoClip
    /// </summary>
    public VideoClip VideoClip
    {
        set { model.VideoSource(value); }
    }
    /// <summary>
    /// ������Ƶ�Ƿ�Ϊѭ������
    /// </summary>
    public bool Loop
    {
        set
        {
            model.Loop = value;
        }
        get
        {
            return model.Loop;
        }
    }
    private void Awake()
    {
        model = this.GetComponent<NVideoPlayerModel>();
    }
    /// <summary>
    /// ������Ƶ����
    /// </summary>
    public void Play()
    {
        model.VideoPlay();
    }
    /// <summary>
    /// ������Ƶ��ͣ
    /// </summary>
    public void Pause()
    {
        model.Pause();
    }
    /// <summary>
    /// ������Ƶ��ͣ
    /// </summary>
    public void Stop()
    {
        model.Stop();
    }
}