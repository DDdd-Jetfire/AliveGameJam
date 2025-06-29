using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWindow : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 offset;

    void Awake()
        {
        rectTransform = GetComponent<RectTransform>();

        // 自动找最近的 Canvas（可手动设置更精准喵）
        canvas = GetComponentInParent<Canvas>();
        }

    public void OnBeginDrag(PointerEventData eventData)
        {
        // 记录点击点与窗口左下角的偏移量
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, eventData.position, eventData.pressEventCamera, out offset);
        }

    public void OnDrag(PointerEventData eventData)
        {
        Vector2 mousePos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out mousePos))
            {
            // 把窗口的位置设置为鼠标位置 - 初始点击偏移
            rectTransform.localPosition = mousePos - offset;
            }
        }
    }