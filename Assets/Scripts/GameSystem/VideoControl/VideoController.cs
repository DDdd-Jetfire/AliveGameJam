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


    void Start()
    {
        vp = gameObject.GetComponent<VideoPlayer>();
        mr = gameObject.GetComponent<MeshRenderer>();
        vp.renderMode = VideoRenderMode.RenderTexture;
        vp.isLooping = true;
        //ConfigureVideoSource();
        //vp.targetTexture = rt;
        vp.Prepare();

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
    private void OnVideoPrepared(VideoPlayer vp)
    {
        vp.Play();
    }

}