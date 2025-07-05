using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]



public class ColorTintButton : MonoBehaviour
    {/// <summary>
     /// 用来给sprite renderer加按钮效果的
     /// </summary>
    [Header("状态颜色")]
    public Color normalColor = Color.white;
    public Color highlightedColor = new Color(0.8f, 0.8f, 0.8f, 1);
    public Color pressedColor = Color.white;
    public Color disabledColor = Color.grey;

    [Header("是否可交互")]
    public bool interactable = true;

    [Header("点击事件")]
    public UnityEvent onClick;

    private SpriteRenderer spriteRenderer;
    private bool isMouseOver = false;

    void Awake()
        {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateColor(normalColor);
        }

    void OnMouseEnter()
        {
        if (!interactable) return;
        isMouseOver = true;
        UpdateColor(highlightedColor);
        }

    void OnMouseExit()
        {
        if (!interactable) return;
        isMouseOver = false;
        UpdateColor(normalColor);
        }

    void OnMouseDown()
        {
        if (!interactable) return;
        UpdateColor(pressedColor);
        }

    void OnMouseUp()
        {
        if (!interactable) return;
        if (isMouseOver)
            {
            onClick?.Invoke();
            UpdateColor(highlightedColor);
            }
        else
            {
            UpdateColor(normalColor);
            }
        }

    public void SetInteractable(bool value)
        {
        interactable = value;
        UpdateColor(value ? normalColor : disabledColor);
        }

    private void UpdateColor(Color color)
        {
        if (spriteRenderer != null)
            {
            spriteRenderer.color = color;
            }
        }
    }