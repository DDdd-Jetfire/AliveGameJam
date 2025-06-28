using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{

    public static InteractManager instance;

    public GameObject cursor;
    public Vector2 hotspot = Vector2.zero; // 光标位置
    public Vector2 currentDir = Vector2.zero;
    private Vector2 lastPos = Vector2.zero;
    [Header("Dir Settings")]
    public float minMoveDistance = 0.1f;

    [Header("Interact Settings")]
    public LayerMask interactableLayer; // 可交互物体层级
    public float rayDistance = 2f; // 射线检测距离
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

        // 发射射线
        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            rayDistance,
            interactableLayer
        );

        // 调试绘制
        Debug.DrawRay(origin, Vector2.down * (hit ? hit.distance : rayDistance), Color.red, 1f);

        // 处理射线命中的物体
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
