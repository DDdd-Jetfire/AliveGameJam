using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{

    public static InteractManager instance;

    public GameObject cursor;
    public Vector2 hotspot = Vector2.zero; // ���λ��
    public Vector2 currentDir = Vector2.zero;
    private Vector2 lastPos = Vector2.zero;
    [Header("Dir Settings")]
    public float minMoveDistance = 0.1f;

    [Header("Interact Settings")]
    public LayerMask interactableLayer; // �ɽ�������㼶
    public float rayDistance = 2f; // ���߼�����
    private enum CursorState
    {
        able,
        disable,
    }

    private CursorState currentState = CursorState.able;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Cursor.visible = false;

    }

    void Update()
    {
        hotspot = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(currentState == CursorState.able)
        {
            cursor.transform.position = hotspot;
        }
        else
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootVerticalRay();
        }

        if (Vector2.Distance(lastPos, hotspot) > minMoveDistance)
        {
            currentDir = (hotspot - lastPos).normalized;
            lastPos = hotspot;
        }
    }

    public void SetAble()
    {
        currentState = CursorState.able;
    }

    public void SetDisable()
    {
        currentState = CursorState.disable;
    }

    void ShootVerticalRay()
    {
        Vector2 origin = cursor.transform.position;

        // ��������
        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            rayDistance,
            interactableLayer
        );

        // ���Ի���
        Debug.DrawRay(origin, Vector2.down * (hit ? hit.distance : rayDistance), Color.red, 1f);

        // �����������е�����
        if (hit.collider != null)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Click();
            }
        }
    }
}
