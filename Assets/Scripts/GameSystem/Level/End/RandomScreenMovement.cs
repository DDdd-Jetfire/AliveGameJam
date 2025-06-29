using UnityEngine;

public class RandomScreenMovement : MonoBehaviour
{
    [Header("运动参数")]
    public float moveSpeed = 3f;         // 基础移动速度
    public float speedVariation = 1f;    // 速度变化范围
    public float minStopTime = 0.5f;     // 最短停留时间
    public float maxStopTime = 2f;       // 最长停留时间

    private Vector3 targetPosition;      // 目标位置
    private float currentSpeed;          // 当前移动速度
    private float stopTimer;             // 停留计时器
    private Camera mainCamera;           // 主摄像机
    private SpriteRenderer sprite;       // 可选：用于计算物体尺寸

    void Start()
    {
        mainCamera = Camera.main;

        // 尝试获取SpriteRenderer用于计算物体尺寸
        sprite = GetComponent<SpriteRenderer>();

        // 设置初始目标位置
        SetNewRandomTarget();
    }

    void Update()
    {
        // 如果到达目标位置或停留时间结束
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f || stopTimer <= 0)
        {
            // 停留计时
            if (stopTimer > 0)
            {
                stopTimer -= Time.deltaTime;
                return;
            }

            // 获取新目标位置
            SetNewRandomTarget();
        }

        // 向目标位置移动
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            currentSpeed * Time.deltaTime
        );
    }

    // 设置新的随机目标位置
    void SetNewRandomTarget()
    {
        // 计算考虑物体尺寸的屏幕边界
        Vector4 screenBounds = CalculateScreenBounds();

        // 生成随机目标位置（屏幕范围内）
        targetPosition = new Vector3(
            Random.Range(screenBounds.x, screenBounds.y),
            Random.Range(screenBounds.z, screenBounds.w),
            0
        );

        // 设置随机速度
        currentSpeed = moveSpeed + Random.Range(-speedVariation, speedVariation);

        // 设置随机停留时间
        stopTimer = Random.Range(minStopTime, maxStopTime);
    }

    // 计算考虑物体尺寸的屏幕边界
    Vector4 CalculateScreenBounds()
    {
        Vector3 screenMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        float objectWidth = 0;
        float objectHeight = 0;

        // 如果有SpriteRenderer，计算物体尺寸
        if (sprite != null)
        {
            objectWidth = sprite.bounds.extents.x;
            objectHeight = sprite.bounds.extents.y;
        }

        return new Vector4(
            screenMin.x + objectWidth,
            screenMax.x - objectWidth,
            screenMin.y + objectHeight,
            screenMax.y - objectHeight
        );
    }

    // 可选：在Scene视图中显示目标位置
    void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(targetPosition, 0.2f);
            Gizmos.DrawLine(transform.position, targetPosition);
        }
    }
}