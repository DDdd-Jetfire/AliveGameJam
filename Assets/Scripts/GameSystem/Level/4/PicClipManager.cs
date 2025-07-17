using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicClipManager : ClipManager
{

    public string receivedEventName = "TriggerBikeButton";
    protected override void Start()
    {
        PicSquare[] lc = gameObject.transform.GetComponentsInChildren<PicSquare>();
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
            //GameManager.instance.GoToNextScene(nextScene.name);
        }
        else
        {
            LevelSetFault();
            ap.PlaySoundEffects(1);
        }
        LevelCa();
        GameManager.instance.GoToNextScene(nextScene);
    }

    protected override void LevelCa()
    {
        if (levelState)
        {
            GameManager.instance.AddHumanValue(25);
        }
        else
        {
            GameManager.instance.AddHumanValue(-10);
        }
    }



    protected override void OnDestroy()
    {
        //GlobalEventManager.instance.UnregisterEvent(receivedEventName, CheckAll);
        //GlobalEventManager.instance.UnregisterEvent(onVideoEventName, OnVideoFinnished);
    }
}
