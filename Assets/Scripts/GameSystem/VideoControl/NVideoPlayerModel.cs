using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using System;

public class NVideoPlayerModel : MonoBehaviour
{
    [SerializeField]
    [Header("自适应模式")]
    private bool selfAdaption = false;
    [SerializeField]
    [Header("是否循环播放")]
    private bool loop;
    [SerializeField]
    [Header("是否自动播放")]
    private bool autoPlay;
    [SerializeField]
    private VideoPlayer videoPlayer;
    //[SerializeField]
    //private RenderTexture renderTexture;
    private RenderTexture rt;
    private RenderTextureFormat textureFormat = RenderTextureFormat.ARGB32;
    [SerializeField]
    private Vector2 renderTextureSize;
    bool videoPrepared = false;
    bool isVideoPlayEnd = false;

    public double VideoLength
    {
        get
        {
            return (videoPlayer.clip.length);
        }
    }
    public double PlayTime
    {
        set
        {
            videoPlayer.time = value;
        }
        get { return videoPlayer.time; }
    }
    public VideoSource Source
    {
        set { videoPlayer.source = value; }
        get { return videoPlayer.source; }
    }
    public string Url
    {
        set { videoPlayer.url = value; videoPrepared = false; }
        get { return videoPlayer.url; }
    }
    public VideoClip Clip
    {
        set { videoPlayer.clip = value; videoPrepared = false; }
        get { return videoPlayer.clip; }
    }
    public bool IsVideoPlaying
    {
        get { return videoPlayer.isPlaying; }
    }
    public bool Loop
    {
        set
        {
            videoPlayer.isLooping = value;
        }
        get
        {
            return videoPlayer.isLooping;
        }
    }

    private RectTransform rectTransform;

    private Vector2 oriSize;
    private Vector3 oriPostion;
    private int screenWidth;
    private int screenHeight;
    private AudioSource audioSource;
    int cullMask;
    Canvas parent;

    private void Awake()
    {
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        Init();
        if (autoPlay)
        {
            AutoPlay();
        }
        else
        {
            Stop();
        }
    }
    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        Loop = loop;
        parent = transform.GetComponentInParent<Canvas>();
        rectTransform = this.GetComponent<RectTransform>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        oriSize = rectTransform.sizeDelta;
        oriPostion = rectTransform.localPosition;


        rt = new RenderTexture(
            (int)renderTextureSize.x,
            (int)renderTextureSize.y,
            24,
            textureFormat
        );
        rt.name = "VideoRenderTexture";
        rt.depthStencilFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.D32_SFloat_S8_UInt;


        videoPlayer.targetTexture = rt;
        GetComponentInChildren<RawImage>().texture = rt;
        audioSource = videoPlayer.GetComponent<AudioSource>();
    }
    /// <summary>
    /// 如果初始有视频资源，走正常流程播放
    /// </summary>
    public void AutoPlay()
    {
        if (IsVideoPlaying && !videoPrepared)
        {
            Stop();
        }
        if (videoPlayer.source == UnityEngine.Video.VideoSource.Url)
        {
            if (!string.IsNullOrEmpty(videoPlayer.url))
            {
                VideoSource(videoPlayer.url);
            }
        }
        else
        {
            if (null != videoPlayer.clip)
            {
                VideoSource(videoPlayer.clip);
            }
        }
    }
    /// <summary>
    /// 全屏
    /// </summary>
    public void FullScreen(bool full)
    {
        if (full)
        {
            //全屏
            if (selfAdaption)
            {
                rectTransform.sizeDelta = Vector2.zero;
            }
            else
            {
                rectTransform.sizeDelta = new Vector2(screenWidth, screenHeight);
            }
            parent.enabled = false;
            rectTransform.localPosition = Vector3.zero;
            Canvas can = this.gameObject.AddComponent<Canvas>();
            can.overrideSorting = true;
            can.sortingOrder = 20;
            this.gameObject.AddComponent<GraphicRaycaster>();
            if (null != Camera.main)
            {
                cullMask = Camera.main.cullingMask;
                Camera.main.cullingMask = 0;
            }
        }
        else
        {
            //非全屏
            rectTransform.sizeDelta = oriSize;
            rectTransform.localPosition = oriPostion;
            parent.enabled = true;
            Destroy(this.gameObject.GetComponent<GraphicRaycaster>());
            Destroy(this.gameObject.GetComponent<Canvas>());
            if (null != Camera.main)
            {
                Camera.main.cullingMask = cullMask;
            }
        }
    }

    /// <summary>
    /// 视频播放
    /// </summary>
    public void VideoPlay()
    {
        if (!videoPrepared)
        {
            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += (VideoPlayer video) =>
            {
                videoPrepared = true;
                isVideoPlayEnd = false;
                VideoPlay();
            };
            return;
        }
        videoPlayer.Play();
        isVideoPlayEnd = false;
    }
    /// <summary>
    /// 视频暂停
    /// </summary>
    public void Pause()
    {
        videoPlayer.Pause();
    }
    /// <summary>
    /// 视频停止
    /// </summary>
    public void Stop()
    {
        videoPlayer.Stop();
        isVideoPlayEnd = true;
        PlayTime = 0f;
    }
    /// <summary>
    /// 设置视频链接
    /// </summary>
    /// <param name="url"></param>
    public void VideoSource(string url)
    {
        Source = UnityEngine.Video.VideoSource.Url;
        Url = url;
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += VideoComplete;
    }
    /// <summary>
    /// 设置视频资源
    /// </summary>
    /// <param name="clip"></param>
    public void VideoSource(VideoClip clip)
    {
        if (IsVideoPlaying)
        {
            Stop();
        }
        Source = UnityEngine.Video.VideoSource.VideoClip;
        Clip = clip;
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += VideoComplete;
    }
    void VideoComplete(VideoPlayer video)
    {
        videoPrepared = true;
        isVideoPlayEnd = false;
        if (!autoPlay) return;
        VideoPlay();
    }
    /// <summary>
    /// 更新进度条和时间
    /// </summary>
    private void FixedUpdate()
    {
        if (IsVideoPlaying)
        {
            if (VideoLength - PlayTime < 0.1f)
            {
                if (!Loop)
                {
                    Stop();
                }
            }
        }
    }

    /// <summary>
    /// 手动调节进度，按下时暂停播放
    /// </summary>
    public void SliderPointerDown()
    {
        Pause();
    }
    /// <summary>
    /// 手动调节进度，抬起时继续播放
    /// </summary>
    public void SliderPointerUp()
    {
        isVideoPlayEnd = false;
        VideoPlay();
    }


    /// <summary>
    /// 格式化播放时间
    /// </summary>
    //public string GetVideoTime(double playTime)
    //{
    //    if (VideoLength >= 3600)
    //    {
    //        int clipHour = (int)(playTime) / 3600;
    //        int clipMinute = (int)(playTime - clipHour * 3600) / 60;
    //        int clipSecond = (int)(playTime - clipHour * 3600 - clipMinute * 60);
    //        return string.Format("{0:D2}:{1:D2}:{2:D2}", clipHour, clipMinute, clipSecond);
    //    }
    //    else
    //    {
    //        int clipMinute = (int)(playTime) / 60;
    //        int clipSecond = (int)(playTime - clipMinute * 60);
    //        return string.Format("{0:D2}:{1:D2}", clipMinute, clipSecond);
    //    }
    //}
    private void OnDisable()
    {
        rt.Release();
        videoPlayer.prepareCompleted -= VideoComplete;
    }
    private void OnDestroy()
    {
        rt.Release();
        videoPlayer.prepareCompleted -= VideoComplete;
    }
    private static long unixBaseMillis = new DateTime(1970, 1, 1, 0, 0, 0).ToFileTimeUtc() / 10000;
    long GetTimeMillion()
    {
        return (DateTime.Now.ToFileTimeUtc() / 10000) - unixBaseMillis;
    }
}