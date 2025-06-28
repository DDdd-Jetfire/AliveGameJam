using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour
    {
    private Sequence currentSequence;
    float currentX;
    Vector3 originalPos;
    private void Start()
        {
        currentX = this.transform.position.x;
        originalPos = this.transform.position;
        }

    public void Animation_Rotate()
        {
        // 如果上一个旋转还在，先杀掉（可选）
        //transform.DOKill();  // 只杀死该对象上的 tween，不影响位移动画

        transform.DORotate(
            new Vector3(0, 0, -360),  // 逆时针旋转
            1f,
            RotateMode.FastBeyond360
        ).SetEase(Ease.Linear);
        }

    public void Animation_Back()
        {
        //KillCurrentSequence();
        Debug.Log("后了");



        currentSequence = DOTween.Sequence();
        currentSequence.Append(transform.DOMoveX(currentX - 0.3f, 0.3f).SetEase(Ease.InQuad));
        currentSequence.Append(transform.DOMoveX(currentX, 0.3f).SetEase(Ease.OutQuad));
        }

    public void Animation_Forward()
        {
        //KillCurrentSequence();
        Debug.Log("移了");


        currentSequence = DOTween.Sequence();
        currentSequence.Append(transform.DOMoveX(currentX + 0.3f, 0.3f).SetEase(Ease.InQuad));
        currentSequence.Append(transform.DOMoveX(currentX, 0.3f).SetEase(Ease.OutQuad));
        }

    public void PlayBounceEffect()
        {

        // DoTween 动画序列
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOLocalMoveX(originalPos.x + 10f, 0.3f)
            .SetEase(Ease.OutQuad)) // 减速移动（easeOut）
           .Append(transform.DOLocalMoveX(originalPos.x - 10f, 0.3f)
            .SetEase(Ease.InQuad)) // 加速回来（easeIn）

           .OnComplete(() =>
           {
               // 确保回原位以防数值漂移
               //transform.localPosition = originalPos;
           });
        }

    public void PlayBounceEffect2()
        {


        // DoTween 动画序列
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOLocalMoveX(originalPos.x - 10f, 0.3f)
            .SetEase(Ease.OutQuad)) // 减速移动（easeOut）
           .Append(transform.DOLocalMoveX(originalPos.x + 10f, 0.3f)
            .SetEase(Ease.InQuad)) // 加速回来（easeIn）

           .OnComplete(() =>
           {
               // 确保回原位以防数值漂移
               //transform.localPosition = originalPos;
           });
        }


    private void KillCurrentSequence()
        {
        if (currentSequence != null && currentSequence.IsActive())
            {
            currentSequence.Kill();
            currentSequence = null;
            }
        }
    }



