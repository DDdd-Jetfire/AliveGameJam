using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeClipManager : MonoBehaviour
{
    public List<BikeSquare> bikeSquareList = new List<BikeSquare>();

    public List<bool> isCorrectTarget1st = new List<bool>();
    public List<bool> isCorrectTarget2st = new List<bool>();

    public enum BikeClipState
    {
        sta1,
        sta2,
    }
    public BikeClipState bcs = BikeClipState.sta1;

    public string receivedEventName = "TriggerBikeButton";
    public string onVideoEventName = "VideoFinnished";
    public string finnishEventName = "PuzzleFinnished";
    public string rideBikeEventName = "RideBike";
    void Start()
    {
        GlobalEventManager.instance.RegisterEvent(receivedEventName, CheckAll);
        GlobalEventManager.instance.RegisterEvent(onVideoEventName, OnVideoFinnished);
        BikeSquare[] bs = gameObject.transform.GetComponentsInChildren<BikeSquare>();
        foreach(var b in bs)
        {
            bikeSquareList.Add(b);
        }
        SetAs1st();
    }
    void CheckAll()
    {
        switch (bcs)
        {
            case BikeClipState.sta1:

                bool isAllSelect = true;
                for (int i = 0; i < bikeSquareList.Count; i++)
                {
                    if (isCorrectTarget1st[i] != bikeSquareList[i].isSelect)
                    {
                        isAllSelect = false;
                    }
                }
                if (isAllSelect)
                {
                    GlobalEventManager.instance.TriggerEvent(rideBikeEventName);
                }
                break;
            case BikeClipState.sta2:

                bool isAllSelect2 = true;
                for (int i = 0; i < bikeSquareList.Count; i++)
                {
                    if (isCorrectTarget2st[i] != bikeSquareList[i].isSelect)
                    {
                        isAllSelect2 = false;
                    }
                }
                if (isAllSelect2)
                {
                    GlobalEventManager.instance.TriggerEvent(finnishEventName);
                }
                break;
        }
    }

    void OnVideoFinnished()
    {
        ResetPuzzle();
        SetAs2st();
        bcs = BikeClipState.sta2;
    }

    void Update()
    {
        
    }

    private void SetAs1st()
    {
        for (int i = 0; i < bikeSquareList.Count; i++)
        {
            if (isCorrectTarget1st[i])
            {
                bikeSquareList[i].isCorrectSquare = true;
            }
        }
    }

    private void ResetPuzzle()
    {
        for (int i = 0; i < bikeSquareList.Count; i++)
        {
            if (isCorrectTarget1st[i])
            {
                bikeSquareList[i].ResetPuzzle();
            }
        }
    }
    private void SetAs2st()
    {
        for (int i = 0; i < bikeSquareList.Count; i++)
        {
            if (isCorrectTarget2st[i])
            {
                bikeSquareList[i].isCorrectSquare = true;
            }
        }
    }

    private void OnDestroy()
    {
        GlobalEventManager.instance.UnregisterEvent(receivedEventName, CheckAll);
        GlobalEventManager.instance.UnregisterEvent(onVideoEventName, OnVideoFinnished);
    }
}
