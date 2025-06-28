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

        // ����SpriteRenderer�Ĳ���
        mr.material = new Material(Shader.Find("Unlit/Texture"));
        mr.material.mainTexture = rt;

        // ׼����������Ƶ
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
        // �����һ֡
        StartCoroutine(HandleFirstFrame());
        //vp.Play();
    }

    IEnumerator HandleFirstFrame()
    {
        // ���õ���һ֡
        vp.frame = 0;

        // ��Ⱦ��һ֡
        vp.Play();
        vp.StepForward();
        vp.Pause();

        // �ȴ�һ֡ȷ����Ⱦ
        yield return null;

        // �Զ�����
        if (autoPlayAfterLoad)
        {
            yield return new WaitForSeconds(0.1f); // �����ӳ�
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