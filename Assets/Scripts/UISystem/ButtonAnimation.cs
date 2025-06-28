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
        // �����һ����ת���ڣ���ɱ������ѡ��
        //transform.DOKill();  // ֻɱ���ö����ϵ� tween����Ӱ��λ�ƶ���

        transform.DORotate(
            new Vector3(0, 0, -360),  // ��ʱ����ת
            1f,
            RotateMode.FastBeyond360
        ).SetEase(Ease.Linear);
        }

    public void Animation_Back()
        {
        //KillCurrentSequence();
        Debug.Log("����");



        currentSequence = DOTween.Sequence();
        currentSequence.Append(transform.DOMoveX(currentX - 0.3f, 0.3f).SetEase(Ease.InQuad));
        currentSequence.Append(transform.DOMoveX(currentX, 0.3f).SetEase(Ease.OutQuad));
        }

    public void Animation_Forward()
        {
        //KillCurrentSequence();
        Debug.Log("����");


        currentSequence = DOTween.Sequence();
        currentSequence.Append(transform.DOMoveX(currentX + 0.3f, 0.3f).SetEase(Ease.InQuad));
        currentSequence.Append(transform.DOMoveX(currentX, 0.3f).SetEase(Ease.OutQuad));
        }

    public void PlayBounceEffect()
        {

        // DoTween ��������
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOLocalMoveX(originalPos.x + 10f, 0.3f)
            .SetEase(Ease.OutQuad)) // �����ƶ���easeOut��
           .Append(transform.DOLocalMoveX(originalPos.x - 10f, 0.3f)
            .SetEase(Ease.InQuad)) // ���ٻ�����easeIn��

           .OnComplete(() =>
           {
               // ȷ����ԭλ�Է���ֵƯ��
               //transform.localPosition = originalPos;
           });
        }

    public void PlayBounceEffect2()
        {


        // DoTween ��������
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOLocalMoveX(originalPos.x - 10f, 0.3f)
            .SetEase(Ease.OutQuad)) // �����ƶ���easeOut��
           .Append(transform.DOLocalMoveX(originalPos.x + 10f, 0.3f)
            .SetEase(Ease.InQuad)) // ���ٻ�����easeIn��

           .OnComplete(() =>
           {
               // ȷ����ԭλ�Է���ֵƯ��
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



