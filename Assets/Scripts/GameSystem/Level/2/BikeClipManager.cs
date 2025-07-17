using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BikeClipManager : ClipManager
{
    public List<bool> isCorrectTarget2st = new List<bool>();


    public string receivedEventName = "TriggerBikeButton";
    public string onVideoEventName = "VideoFinnished";
    public string finnishEventName = "PuzzleFinnished";
    public string rideBikeEventName = "RideBike";

    public enum BikeClipState
    {
        sta1,
        sta2,
    }
    public BikeClipState bcs = BikeClipState.sta1;

    protected override void Start()
    {
        //GlobalEventManager.instance.RegisterEvent(receivedEventName, CheckAll);
        GlobalEventManager.instance.RegisterEvent(onVideoEventName, OnVideoFinnished);
        BikeSquare[] bs = gameObject.transform.GetComponentsInChildren<BikeSquare>();
        foreach (var b in bs)
        {
            SquareList.Add(b);
        }
        SetAs1st();
    }

    protected override void CheckAll()
    {
        if (!InteractManager.instance.canInteract)
        {
            return;
        }
        switch (bcs)
        {
            case BikeClipState.sta1:

                InteractManager.instance.SetDisable();
                bool isAllSelect = true;
                for (int i = 0; i < SquareList.Count; i++)
                {
                    if (isCorrectTarget1st[i])
                    {
                        if (!SquareList[i].isSelect)
                        {
                            SquareList[i].SetFalse();
                            isAllSelect = false;
                        }
                    }
                    else
                    {
                        if (SquareList[i].isSelect)
                        {
                            SquareList[i].SetFalse();
                        }
                    }
                }
                if (isAllSelect)
                {
                    ap.PlaySoundEffects(0);
                    GlobalEventManager.instance.TriggerEvent(rideBikeEventName);
                }
                else
                {
                    LevelSetFault();
                    ap.PlaySoundEffects(1);
                    InteractManager.instance.SetAble();
                }
                break;
            case BikeClipState.sta2:

                InteractManager.instance.SetDisable();
                bool isAllSelect2 = true;
                for (int i = 0; i < SquareList.Count; i++)
                {
                    if (isCorrectTarget2st[i])
                    {
                        if (!SquareList[i].isSelect)
                        {
                            SquareList[i].SetFalse();
                            isAllSelect2 = false;
                        }
                    }
                    else
                    {
                        if (SquareList[i].isSelect)
                        {
                            SquareList[i].SetFalse();
                        }
                    }
                }

                if (isAllSelect2)
                {
                    //GlobalEventManager.instance.TriggerEvent(finnishEventName);
                    ap.PlaySoundEffects(0);
                }
                else
                {
                    ap.PlaySoundEffects(1);
                    LevelSetFault();
                    InteractManager.instance.SetAble();
                }
                LevelCa();
                GameManager.instance.GoToNextScene(nextScene);
                break;
        }
    }


    protected override void OnVideoFinnished()
    {
        ResetPuzzle();
        SetAs2st();
        bcs = BikeClipState.sta2;
        InteractManager.instance.SetAble();
    }

    protected override void ResetPuzzle()
    {
        for (int i = 0; i < SquareList.Count; i++)
        {
            SquareList[i].ResetPuzzle();
        }
    }

    protected void SetAs2st()
    {
        for (int i = 0; i < SquareList.Count; i++)
        {
            if (isCorrectTarget2st[i])
            {
                SquareList[i].isCorrectSquare = true;
            }
        }
    }
    protected override void OnDestroy()
    {
        GlobalEventManager.instance.UnregisterEvent(onVideoEventName, OnVideoFinnished);
    }
}
