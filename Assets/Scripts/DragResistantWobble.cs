using UnityEngine;

public class DragResistantWobble : MonoBehaviour
{
    public float resistance = 0.1f;   // 抗拒程度（0.0 ~ 1.0）
    public float wobbleAmount = 0.2f; // 最大颤抖幅度
    public float returnSpeed = 5f;    // 回弹速度

    private Vector3 targetOffset = Vector3.zero;
    private Vector3 lastMousePosition;

    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        Vector3 currentMousePos = Input.mousePosition;
        Vector3 deltaMouse = currentMousePos - lastMousePosition;
        lastMousePosition = currentMousePos;

        // 如果鼠标有移动
        if (deltaMouse.magnitude > 0.01f)
        {
            // 鼠标方向 * -resistance = 颤抖方向
            Vector3 antiDirection = -deltaMouse.normalized * wobbleAmount;

            // 添加到目标偏移（使用阻尼防止抖动过大）
            targetOffset = Vector3.Lerp(targetOffset, antiDirection, resistance);
        }

        // 回弹到默认位置（平滑）
        targetOffset = Vector3.Lerp(targetOffset, Vector3.zero, Time.deltaTime * returnSpeed);

        // 应用偏移
        transform.localPosition = targetOffset;
    }
}