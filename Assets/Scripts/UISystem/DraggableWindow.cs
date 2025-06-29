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

        // �Զ�������� Canvas�����ֶ����ø���׼����
        canvas = GetComponentInParent<Canvas>();
        }

    public void OnBeginDrag(PointerEventData eventData)
        {
        // ��¼������봰�����½ǵ�ƫ����
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, eventData.position, eventData.pressEventCamera, out offset);
        }

    public void OnDrag(PointerEventData eventData)
        {
        Vector2 mousePos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out mousePos))
            {
            // �Ѵ��ڵ�λ������Ϊ���λ�� - ��ʼ���ƫ��
            rectTransform.localPosition = mousePos - offset;
            }
        }
    }