using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEditor;

public class UIAnimation : MonoBehaviour
    {

    private Image image;
    private SpriteRenderer spriteRenderer;
    [Tooltip("等待开始淡出时间，默认0.5秒")]
    public float wait = 0.5f;
    [Tooltip("淡出时间，默认0.5秒")]
    public float fadeDuration = 0.5f;


    void Awake()
        {
        // 找到 Image 组件喵
        image = GetComponent<Image>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (image == null && spriteRenderer == null)
            {
            Debug.LogError("没有可控的显示组件");
            }
        }

    public void HidePanel()
        {
        if (image == null && spriteRenderer == null) return;
        Sequence se = DOTween.Sequence();
        if (image != null)
            {
            se.AppendInterval(wait);
            se.Append
                (
                image.DOFade(0f, fadeDuration).OnComplete
                    (
                        () => { gameObject.SetActive(false); }
                    )
                );
            }
        if (spriteRenderer != null)
            {
            se.AppendInterval(wait);
            se.Append
                (
                spriteRenderer.DOFade(0f, fadeDuration).OnComplete
                    (
                        () => { gameObject.SetActive(false); }
                    )
                );
            }
        }

    public void ShowPanel()
        {
        if (image == null && spriteRenderer == null) return;
        Sequence se = DOTween.Sequence();
        // 先把面板打开 + alpha设为0
        if (image != null)
            {
            gameObject.SetActive(true);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);

            se.AppendInterval(wait);
            // 然后淡入
            se.Append(image.DOFade(1f, fadeDuration));

            }
        else
            {
            gameObject.SetActive(true);
            Color c = spriteRenderer.color;
            c.a = 0f;
            spriteRenderer.color = c;
            spriteRenderer.DOFade(1f, fadeDuration);
            }
        }
    }

