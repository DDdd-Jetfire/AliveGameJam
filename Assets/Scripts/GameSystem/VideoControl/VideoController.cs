using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoController : MonoBehaviour
{
    public VideoClip vc;
    public VideoPlayer vp;

    public RenderTexture rt;
    public MeshRenderer mr;
    public Vector2 renderTextureSize = new Vector2(1920, 1080);
    public RenderTextureFormat textureFormat = RenderTextureFormat.ARGB32;

    private bool isPrepareComplete = false;


    public bool autoPlayAfterLoad = false;
    public bool isVideoLoop = true;


    void Start()
    {
        vp = gameObject.GetComponent<VideoPlayer>();
        mr = gameObject.GetComponent<MeshRenderer>();
        vp.renderMode = VideoRenderMode.RenderTexture;
        vp.isLooping = isVideoLoop;
        //ConfigureVideoSource();
        //vp.targetTexture = rt;

        rt = new RenderTexture(
            (int)renderTextureSize.x,
            (int)renderTextureSize.y,
            24,
            textureFormat
        );
        rt.name = "VideoRenderTexture";

        vp.playOnAwake = false;
        vp.clip = vc;
        vp.renderMode = VideoRenderMode.RenderTexture;
        vp.targetTexture = rt;

        // 设置SpriteRenderer的材质
        mr.material = new Material(Shader.Find("Unlit/Texture"));
        mr.material.mainTexture = rt;

        // 准备并播放视频
        vp.Prepare();
        vp.prepareCompleted += OnVideoPrepared;
    }

    private void Update()
    {

    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        Debug.Log($"{gameObject.name}+ Prepare Complete!");
        isPrepareComplete = true;
        // 处理第一帧
        StartCoroutine(HandleFirstFrame());
        //vp.Play();
    }

    IEnumerator HandleFirstFrame()
    {
        // 设置到第一帧
        vp.frame = 0;

        // 渲染第一帧
        vp.Play();
        vp.StepForward();
        vp.Pause();

        // 等待一帧确保渲染
        yield return null;

        // 自动播放
        if (autoPlayAfterLoad)
        {
            yield return new WaitForSeconds(0.1f); // 短暂延迟
            PlayVideo();
        }
    }

    public void PlayVideo()
    {
        if (!isPrepareComplete) return;
        vp.Play();
    }
    public void PauseVideo()
    {
        if (!isPrepareComplete) return;
        vp.Pause();
    }
    public void StopVideo()
    {
        vp.Stop();
    }
    public void SetVideoSpeed(float speed)
    {
        if (vp.canSetPlaybackSpeed)
        {
            Debug.Log($"{gameObject.name} set play speed to {speed}");
            vp.playbackSpeed = speed;
        }
    }
}