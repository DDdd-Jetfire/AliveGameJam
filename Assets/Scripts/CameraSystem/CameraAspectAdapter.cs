using UnityEngine;

public class CameraAspectAdapter : MonoBehaviour
{
    public float referenceAspect = 16f / 9f; // ���ʱ�ο��Ŀ�߱ȣ���16:9��
    public float referenceSize = 5.4f; // ���ʱ��Sizeֵ

    private Camera orthoCamera;

    void Start()
    {
        orthoCamera = GetComponent<Camera>();
        AdaptCameraToAspect();
    }

    // �����Ļ�ߴ���ܱ仯���細��ģʽ������Update�е���
    void Update()
    {
        AdaptCameraToAspect();
    }

    void AdaptCameraToAspect()
    {
        // ��ǰ��Ļ��߱�
        float currentAspect = (float)Screen.width / Screen.height;

        // ����Size��������Ƹ߶ȣ����ݿ�߱Ȳ�������
        orthoCamera.orthographicSize = referenceSize * (referenceAspect / currentAspect);
    }
}