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
    private  float rayDistance = 0.2f; // 射线检测距离
    private enum CursorState
    {
        normal,
        select,
        waiting
    }

    private bool canInteract = true;

    private CursorState currentState = CursorState.normal;

    public float maxRayCD = 0.1f;
    private float rayCD = 0;

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
        Cursor.visible = false;
        hotspot = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (rayCD < 0)
        {
            rayCD = maxRayCD;
            DetectRay();
        }
        else
        {
            rayCD -= Time.deltaTime;
        }

        if(currentState == CursorState.waiting)
        {
            return;
        }
        else
        {
            cursor.transform.position = hotspot;
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
        canInteract = true;
    }

    public void SetDisable()
    {
        canInteract = false;
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

    void DetectRay()
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
            transform.localEulerAngles = new Vector3(0, 0, 90);
            currentState = CursorState.select;
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            currentState = CursorState.normal;
        }
    }
}
