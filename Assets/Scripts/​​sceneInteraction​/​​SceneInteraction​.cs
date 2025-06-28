
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[System.Serializable]
public class InteractionEvent : UnityEvent<GameObject> { }

public class AdvancedInteractionSystem : MonoBehaviour
{
    [Header("交互设置")]
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactionDistance = 10f;
    [SerializeField] private KeyCode toggleMouseKey = KeyCode.LeftControl;

    [Header("视觉效果")]
    [SerializeField] private bool useFadeEffect = true;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private GameObject cursorVisual; // 可自定义的鼠标指针模型

    [Header("事件")]
    public InteractionEvent OnObjectShown;
    public InteractionEvent OnObjectHidden;
    public UnityEvent OnMouseHidden;
    public UnityEvent OnMouseShown;

    private GameObject lastInteractedObject;
    private bool isMouseHidden = false;
    private bool isInteracting = false;
    private CursorLockMode previousLockState;


    private void UpdateMouseVisual()
    {
        if (cursorVisual != null)
        {
            // 同步自定义指针与系统鼠标状态
            cursorVisual.SetActive(Cursor.visible);

            // 如果是3D指针，更新其位置
            if (cursorVisual.GetComponent<RectTransform>() == null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100f))
                {
                    cursorVisual.transform.position = hit.point + hit.normal * 0.1f;
                }
            }
            else // UI指针
            {
                cursorVisual.transform.position = Input.mousePosition;
            }
        }
    }
    void Start()
    {

        UpdateMouseVisual();
        previousLockState = Cursor.lockState;
    }

    void Update()
    {
        HandleMouseToggle();
        HandleObjectInteraction();
    }

    private void HandleMouseToggle()
    {
        if (Input.GetKeyDown(toggleMouseKey))
        {
            ToggleMouse(!isMouseHidden);
        }
    }

    private void HandleObjectInteraction()
    {
        if (isInteracting || isMouseHidden) return;

        if (Input.GetMouseButtonDown(0))
        {
            TryInteractWithObject();
        }
    }

    // === 鼠标控制核心方法 ===
    public void ToggleMouse(bool hide)
    {
        isMouseHidden = hide;

        // 系统鼠标控制
        Cursor.visible = !hide;
        Cursor.lockState = hide ? CursorLockMode.Locked : previousLockState;

        // 自定义指针控制
        if (cursorVisual != null)
        {
            cursorVisual.SetActive(!hide);
        }

        // 触发事件
        if (hide) OnMouseHidden?.Invoke();
        else OnMouseShown?.Invoke();
    }

    // === 物体交互核心方法 ===
    private void TryInteractWithObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            GameObject targetObj = hit.collider.gameObject;

            if (lastInteractedObject != null && lastInteractedObject != targetObj)
            {
                StartCoroutine(ToggleObject(lastInteractedObject, false));
            }

            StartCoroutine(ToggleObject(targetObj, !targetObj.activeSelf));
            lastInteractedObject = targetObj;
        }
    }

    private IEnumerator ToggleObject(GameObject obj, bool show)
    {
        isInteracting = true;

        if (useFadeEffect && obj.TryGetComponent<CanvasGroup>(out var canvasGroup))
        {
            float targetAlpha = show ? 1f : 0f;
            float startAlpha = canvasGroup.alpha;
            float elapsed = 0f;

            obj.SetActive(true);

            while (elapsed < fadeDuration)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = targetAlpha;
            if (!show) obj.SetActive(false);
        }
        else
        {
            obj.SetActive(show);
        }

        if (show) OnObjectShown?.Invoke(obj);
        else OnObjectHidden?.Invoke(obj);

        isInteracting = false;
    }

    // === 编辑器调试 ===
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 rayEnd = Camera.main.transform.position +
                        Camera.main.transform.forward * interactionDistance;
        Gizmos.DrawLine(Camera.main.transform.position, rayEnd);
    }

    // === 安全清理 ===
    private void OnDestroy()
    {
        // 恢复鼠标状态
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}