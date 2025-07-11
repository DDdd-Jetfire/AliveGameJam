using System;
using UnityEngine;

public class DragAndLimit : MonoBehaviour
{
    // 记录鼠标按下时的位置
    private Vector3 startMousePosition;

    private Vector3 offset;
    // 相机组件引用
    private Camera mainCamera;
    private bool isChoose = false;
    void Start()
    {
        // 获取主相机
        mainCamera = Camera.main;
        DragLayerManager.Instance.RegisterDragObject(gameObject);
    }
    
    void Update()
    {
        // 检测鼠标左键按住
        if (Input.GetMouseButton(0) && isChoose)
        {
            // 计算鼠标移动差值
            Vector3 mouseDelta = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            // 计算新位置
            Vector3 newPosition = mouseDelta + offset;
            
            // 限制物体在屏幕范围内
            newPosition = LimitToScreen(newPosition);
            
            // 应用新位置
            transform.position = newPosition;
        }
    }
    
    // 限制物体在屏幕范围内的方法
    private Vector3 LimitToScreen(Vector3 position)
    {
        // 获取屏幕边界在世界坐标中的值
        Vector3 viewportMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, -mainCamera.transform.position.z));
        Vector3 viewportMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, -mainCamera.transform.position.z));
        
        // 计算物体的尺寸（用于居中限制）
        Vector3 objectSize = GetComponent<Renderer>().bounds.size;
        
        // 限制X轴位置
        position.x = Mathf.Clamp(position.x, 
                               viewportMin.x + objectSize.x / 2, 
                               viewportMax.x - objectSize.x / 2);
        
        // 限制Y轴位置
        position.y = Mathf.Clamp(position.y, 
                               viewportMin.y + objectSize.y / 2, 
                               viewportMax.y - objectSize.y / 2);
        
        // Z轴位置保持不变
        position.z = transform.position.z;
        
        return position;
    }

    private void OnMouseDown()
    {
        offset = - mainCamera.ScreenToWorldPoint(Input.mousePosition) + transform.position;
        DragLayerManager.Instance.UpChooseDragObject(gameObject);
        
        isChoose = true;
    }

    private void OnMouseUp()
    {
        isChoose = false;
    }

    private void OnDestroy()
    {
        DragLayerManager.Instance.UnregisterDragObject(gameObject);
    }
}