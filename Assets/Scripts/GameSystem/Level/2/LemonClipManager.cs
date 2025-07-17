using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonClipManager : ClipManager
{

    public string receivedEventName = "TriggerBikeButton";

    protected override void Start()
    {
        LemonSquare[] lc = gameObject.transform.GetComponentsInChildren<LemonSquare>();
        foreach (var l in lc)
        {
            SquareList.Add(l);
        }
        SetAs1st();
    }

    protected override void CheckAll()
    {
        if (!InteractManager.instance.canInteract)
        {
            return;
        }

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
        }
        else
        {
            LevelSetFault();
            ap.PlaySoundEffects(1);
        }
        LevelCa();
        GameManager.instance.GoToNextScene(nextScene);
    }

    protected override void SetAs1st()
    {
        for (int i = 0; i < SquareList.Count; i++)
        {
            if (isCorrectTarget1st[i])
            {
                SquareList[i].isCorrectSquare = true;
            }
        }
    }


    protected override void OnDestroy()
    {
        //GlobalEventManager.instance.UnregisterEvent(receivedEventName, CheckAll);
        //GlobalEventManager.instance.UnregisterEvent(onVideoEventName, OnVideoFinnished);
    }
}
