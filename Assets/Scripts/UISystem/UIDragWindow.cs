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
        Debug.Log("��ק��ʼ");
        // ���������봰�����Ͻǵ�ƫ����
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
        }
    public void BeginDrag(PointerEventData eventData)
        {
        Debug.Log("��ק��ʼ2");
        // ���������봰�����Ͻǵ�ƫ����
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
        }

    public void OnDrag(PointerEventData eventData)
        {
        Debug.Log("��ק��");
        Vector2 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out var worldPos))
            {
            rectTransform.position = worldPos - (Vector3)offset;
            }
        }
    public void Drag(PointerEventData eventData)
        {
        Debug.Log("��ק��2");
        Vector2 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out var worldPos))
            {
            rectTransform.position = worldPos - (Vector3)offset;
            }
        }
    }