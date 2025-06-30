using UnityEngine;
using UnityEngine.UI;

public class DynamicAspectAdapter : MonoBehaviour
{
    private CanvasScaler scaler;
    public RectTransform rect;
    private float referenceAspect = 16f / 9f; // 基准比例

    private int lastWidth = 1920;
    private int lastHeight = 1080;

    void Start()
    {
        //scaler = GetComponent<CanvasScaler>();
        AdaptToAspect();
    }

    // 窗口大小变化时重适配（如PC窗口化）
    void Update()
        {
            if (Screen.width != lastWidth || Screen.height != lastHeight)
            {
                Debug.Log($"{Screen.width} and {Screen.height}");
                AdaptToAspect();
                lastWidth = Screen.width;
                lastHeight = Screen.height; 
            }
    }

    void AdaptToAspect()
    {
        //if (rect == null)
        //{
        //    Debug.LogError("background not found!");
        //    return;
        //}
        //float currentAspect = (float)Screen.width / Screen.height;
        //
        //scaler.matchWidthOrHeight = (currentAspect > referenceAspect) ? 1 : 0;
        //float currentAspect = (float)Screen.width / Screen.height;
        //scaler.matchWidthOrHeight = (currentAspect > referenceAspect) ? 1 : 0;

        //// 可选：根据比例切换背景图
        //if (Mathf.Approximately(currentAspect, 1.6f))
        //    SetBackgroundFor16_10();
    }
}