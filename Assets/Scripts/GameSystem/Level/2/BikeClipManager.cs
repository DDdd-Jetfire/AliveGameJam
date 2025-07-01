using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    public string nextScene;

    public AudioPlayer ap;
    private bool levelState = true;

    void Start()
    {
        //GlobalEventManager.instance.RegisterEvent(receivedEventName, CheckAll);
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
        if (!InteractManager.instance.canInteract)
        {
            return;
        }
        switch (bcs)
        {
            case BikeClipState.sta1:

                InteractManager.instance.SetDisable();
                bool isAllSelect = true;
                for (int i = 0; i < bikeSquareList.Count; i++)
                {
                    if (isCorrectTarget1st[i])
                    {
                        if (!bikeSquareList[i].isSelect)
                        {
                            bikeSquareList[i].SetFalse();
                            isAllSelect = false;
                        }
                    }
                    else
                    {
                        if (bikeSquareList[i].isSelect)
                        {
                            bikeSquareList[i].SetFalse();
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
                for (int i = 0; i < bikeSquareList.Count; i++)
                {
                    if (isCorrectTarget2st[i])
                    {
                        if (!bikeSquareList[i].isSelect)
                        {
                            bikeSquareList[i].SetFalse();
                            isAllSelect2 = false;
                        }
                    }
                    else
                    {
                        if (bikeSquareList[i].isSelect)
                        {
                            bikeSquareList[i].SetFalse();
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

    private void LevelSetFault()
    {
        levelState = false;
    }

    private void LevelCa()
    {
        if(levelState)
        {
            GameManager.instance.AddHumanValue(25);
        }
        else
        {
            GameManager.instance.AddHumanValue(-10);
        }
    }

    void OnVideoFinnished()
    {
        ResetPuzzle();
        SetAs2st();
        bcs = BikeClipState.sta2;
        InteractManager.instance.SetAble();
    }


    public void NextButtonDown()
    {
        CheckAll();
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
            bikeSquareList[i].ResetPuzzle();
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
        //GlobalEventManager.instance.UnregisterEvent(receivedEventName, CheckAll);
        GlobalEventManager.instance.UnregisterEvent(onVideoEventName, OnVideoFinnished);
    }
}
