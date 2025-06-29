using UnityEngine;

public class RandomScreenMovement : MonoBehaviour
{
    [Header("�˶�����")]
    public float moveSpeed = 3f;         // �����ƶ��ٶ�
    public float speedVariation = 1f;    // �ٶȱ仯��Χ
    public float minStopTime = 0.5f;     // ���ͣ��ʱ��
    public float maxStopTime = 2f;       // �ͣ��ʱ��

    private Vector3 targetPosition;      // Ŀ��λ��
    private float currentSpeed;          // ��ǰ�ƶ��ٶ�
    private float stopTimer;             // ͣ����ʱ��
    private Camera mainCamera;           // �������
    private SpriteRenderer sprite;       // ��ѡ�����ڼ�������ߴ�

    void Start()
    {
        mainCamera = Camera.main;

        // ���Ի�ȡSpriteRenderer���ڼ�������ߴ�
        sprite = GetComponent<SpriteRenderer>();

        // ���ó�ʼĿ��λ��
        SetNewRandomTarget();
    }

    void Update()
    {
        // �������Ŀ��λ�û�ͣ��ʱ�����
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f || stopTimer <= 0)
        {
            // ͣ����ʱ
            if (stopTimer > 0)
            {
                stopTimer -= Time.deltaTime;
                return;
            }

            // ��ȡ��Ŀ��λ��
            SetNewRandomTarget();
        }

        // ��Ŀ��λ���ƶ�
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            currentSpeed * Time.deltaTime
        );
    }

    // �����µ����Ŀ��λ��
    void SetNewRandomTarget()
    {
        // ���㿼������ߴ����Ļ�߽�
        Vector4 screenBounds = CalculateScreenBounds();

        // �������Ŀ��λ�ã���Ļ��Χ�ڣ�
        targetPosition = new Vector3(
            Random.Range(screenBounds.x, screenBounds.y),
            Random.Range(screenBounds.z, screenBounds.w),
            0
        );

        // ��������ٶ�
        currentSpeed = moveSpeed + Random.Range(-speedVariation, speedVariation);

        // �������ͣ��ʱ��
        stopTimer = Random.Range(minStopTime, maxStopTime);
    }

    // ���㿼������ߴ����Ļ�߽�
    Vector4 CalculateScreenBounds()
    {
        Vector3 screenMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        float objectWidth = 0;
        float objectHeight = 0;

        // �����SpriteRenderer����������ߴ�
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

    // ��ѡ����Scene��ͼ����ʾĿ��λ��
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