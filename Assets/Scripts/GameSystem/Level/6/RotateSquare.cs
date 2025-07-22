using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSquare : Square
{
    [Tooltip("旋转速度（度/秒）")]
    private float rotationSpeed = 360f; // 旋转速度

    private bool isRotating = false;
    private Quaternion targetRotation; // 目标旋转角度

    [Tooltip("所属组别")]
    public int belongingGroup = -1;

    [Tooltip("目标旋转物体")]
    public GameObject rotateChild;
    [Tooltip("目标旋转点")]
    public int targetRotateValue = 0;//0,1,2,3,顺时针
    public int currentRotateValue = 0;//0,1,2,3
    [Tooltip("目标旋转点")]
    public bool squareComplete = false;


    enum RotateSquareState
    {
        onRotate,
        onComplete,
    }

    RotateSquareState rss = RotateSquareState.onRotate;

    public NVC nvc;
    public bool isPlayed = false;

    protected override void Start()
    {
        base.Start();
        //写死降低门槛吧,反正应该不会再有别的用途
        clickEventName = "OnSquareClick";
        StartCoroutine(WaitToRegister());
        if(currentRotateValue == targetRotateValue)
        {
            squareComplete = true;
        }
        rotateChild = gameObject.transform.GetChild(0).gameObject;
        // 初始化目标旋转为当前角度
        targetRotation = transform.rotation;
    }

    private IEnumerator WaitToRegister()
    {
        yield return null;
        GlobalEventManager.instance.TriggerEvent<RotateSquare>("RegisterSquareGroup", this);
    }

    public void SetVideoShow()
    {
        //if (group != belongingGroup) return;

        if (!isPlayed)
        {
            if (nvc != null)
            {
                nvc.PlayVideo();
            }
            isPlayed = true;
        }
    }

    public void SetSquareComplete()
    {
        //if (group != belongingGroup) return;

        rss = RotateSquareState.onComplete;
    }

    public override void Click()
    {
        switch (rss)
        {
            case RotateSquareState.onRotate:

                if (!isRotating)
                {
                    // 设置新的目标角度（当前角度 + 90度）
                    targetRotation *= Quaternion.Euler(0, 0, -90);
                    isRotating = true; 
                    squareComplete = false;
                }

                break;
            case RotateSquareState.onComplete:

                if (isRotating) return;

                isSelect = !isSelect;
                if (isSelect)
                {
                    correct.SetActive(true);
                    if (inSelect != null)
                    {
                        spr.sprite = inSelect;
                    }
                    //playAudioHere;

                }
                else
                {
                    if (unSelect != null)
                    {
                        spr.sprite = unSelect;
                    }
                    correct.SetActive(false);
                    fault.SetActive(false);
                }
                GlobalEventManager.instance.TriggerEvent<int>(clickEventName, belongingGroup);
                break;
        }
    }

    void Update()
    {
        if (isRotating)
        {
            // 平滑旋转到目标角度
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            // 当旋转接近目标时停止
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
            {

                transform.rotation = targetRotation;

                isRotating = false;
                currentRotateValue +=1;
                currentRotateValue = currentRotateValue % 4;



                // 创建子物体新的旋转角度
                Quaternion childRotation = Quaternion.Euler(
                    targetRotation.eulerAngles.x,
                    targetRotation.eulerAngles.y,
                    0
                );

                rotateChild.transform.rotation = childRotation;

                if (currentRotateValue == targetRotateValue)
                {
                    squareComplete = true;

                    if (clickEventName != "null" && belongingGroup != -1)
                    {

                        GlobalEventManager.instance.TriggerEvent<int>(clickEventName, belongingGroup);
                    }
                    else
                    {
                        Debug.LogWarning($"setting wrong");
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {

    }
}
