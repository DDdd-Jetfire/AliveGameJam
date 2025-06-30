using UnityEngine;

public class CameraAspectAdapter : MonoBehaviour
{
    public float referenceAspect = 16f / 9f; // 设计时参考的宽高比（如16:9）
    public float referenceSize = 5.4f; // 设计时的Size值

    private Camera orthoCamera;

    void Start()
    {
        orthoCamera = GetComponent<Camera>();
        AdaptCameraToAspect();
    }

    // 如果屏幕尺寸可能变化（如窗口模式），在Update中调用
    void Update()
    {
        AdaptCameraToAspect();
    }

    void AdaptCameraToAspect()
    {
        // 当前屏幕宽高比
        float currentAspect = (float)Screen.width / Screen.height;

        // 调整Size：保持设计高度，根据宽高比差异缩放
        orthoCamera.orthographicSize = referenceSize * (referenceAspect / currentAspect);
    }
}