using UnityEngine;

public class ConstantRotator : MonoBehaviour
    {
    public Vector3 rotationAxis = new Vector3(0, 0, 1); //Ĭ����Z��
    private float rotationSpeed;              //ÿ����ת�Ƕȣ������ʼ����

    void Start()
        {
        // �����ÿ��1Ȧ��360�㣩��3��1Ȧ��120�㣩֮��ѡһ���ٶ�
        float minSpeed = 120f; // 360/ 3��
        float maxSpeed = 360f; // 360/ 1��
        rotationSpeed = Random.Range(minSpeed, maxSpeed);
        }

    void Update()
        {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
        }
    }