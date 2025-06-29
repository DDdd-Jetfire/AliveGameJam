using UnityEngine;

public class ConstantRotator : MonoBehaviour
    {
    public Vector3 rotationAxis = new Vector3(0, 0, 1); //默认绕Z轴
    private float rotationSpeed;              //每秒旋转角度（随机初始化）

    void Start()
        {
        // 随机在每秒1圈（360°）到3秒1圈（120°）之间选一个速度
        float minSpeed = 120f; // 360/ 3秒
        float maxSpeed = 360f; // 360/ 1秒
        rotationSpeed = Random.Range(minSpeed, maxSpeed);
        }

    void Update()
        {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
        }
    }