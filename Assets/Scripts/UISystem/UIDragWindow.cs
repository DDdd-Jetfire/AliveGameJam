using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragWindow : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
    private RectTransform rectTransform;
    private Vector2 offset;

    void Start()
        {
        rectTransform = GetComponent<RectTransform>();
        }

    public void OnBeginDrag(PointerEventData eventData)
        {
        Debug.Log("拖拽开始");
        // 计算点击点与窗口左上角的偏移量
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
        }
    public void BeginDrag(PointerEventData eventData)
        {
        Debug.Log("拖拽开始2");
        // 计算点击点与窗口左上角的偏移量
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
        }

    public void OnDrag(PointerEventData eventData)
        {
        Debug.Log("拖拽中");
        Vector2 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out var worldPos))
            {
            rectTransform.position = worldPos - (Vector3)offset;
            }
        }
    public void Drag(PointerEventData eventData)
        {
        Debug.Log("拖拽中2");
        Vector2 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out var worldPos))
            {
            rectTransform.position = worldPos - (Vector3)offset;
            }
        }
    }