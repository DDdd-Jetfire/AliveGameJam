using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class RoomClipManager : ClipManager
{

    public List<bool> isCorrectTarget2st = new List<bool>();
    public List<bool> isCorrectTarget3st = new List<bool>();

    public enum roomClipState
    {
        sta1,
        sta2,
        sta3,
    }
    public roomClipState rcs = roomClipState.sta1;

    public string onVideo1stEndName = "Video1stFinnished";
    public string onVideo2stEndName = "Video2stFinnished";
    //public string finnishEventName = "PuzzleFinnished";
    public string vc1stEnd = "1stEnd";
    public string vc2stEnd = "2stEnd";
    public TextMeshProUGUI tmpMessage;

    public GameObject vc1st;

    public GameObject tmpCanvas1st;
    public GameObject tmpCanvas2st;


    protected override void Start()
    {
        //GlobalEventManager.instance.RegisterEvent(receivedEventName, CheckAll);
        GlobalEventManager.instance.RegisterEvent(onVideo1stEndName, OnVideo1stFinnished);
        GlobalEventManager.instance.RegisterEvent(onVideo2stEndName, OnVideo2stFinnished);
        BikeSquare[] rs = gameObject.transform.GetComponentsInChildren<BikeSquare>();
        foreach (var r in rs)
        {
            SquareList.Add(r);
        }
        SetAs1st();
        tmpCanvas1st.SetActive(true);
        tmpCanvas2st.SetActive(false);
    }
    protected override void CheckAll()
    {
        if (!InteractManager.instance.canInteract)
        {
            return;
        }

        switch (rcs)
        {
            case roomClipState.sta1:

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
                    GlobalEventManager.instance.TriggerEvent(vc1stEnd);
                    ap.PlaySoundEffects(0);
                }
                else
                {
                    LevelSetFault();
                    InteractManager.instance.SetAble();
                    ap.PlaySoundEffects(1);
                }
                break;
            case roomClipState.sta2:

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
                    GlobalEventManager.instance.TriggerEvent(vc2stEnd);
                    ap.PlaySoundEffects(0);
                }
                else
                {       
                    LevelSetFault();
                    InteractManager.instance.SetAble();
                    ap.PlaySoundEffects(1);
                }
                break;
            case roomClipState.sta3:

                InteractManager.instance.SetDisable();
                bool isAllSelect3 = true;
                for (int i = 0; i < SquareList.Count; i++)
                {
                    if (isCorrectTarget3st[i])
                    {
                        if (!SquareList[i].isSelect)
                        {
                            SquareList[i].SetFalse();
                            //isAllSelect2 = false;
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
                if (isAllSelect3)
                {
                    //GlobalEventManager.instance.TriggerEvent(finnishEventName);

                    ap.PlaySoundEffects(0);
                }
                else
                {
                    LevelSetFault();
                    ap.PlaySoundEffects(1);
                }
                LevelCa();
                GameManager.instance.GoToNextScene(nextScene);
                break;
        }
    }

    protected override void LevelSetFault()
    {
        levelState = false; 
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

    void OnVideo1stFinnished()
    {
        ResetPuzzle();
        SetAs2st();
        rcs = roomClipState.sta2;

        tmpCanvas1st.SetActive(false);
        tmpCanvas2st.SetActive(true);
        InteractManager.instance.SetAble();
    }
    void OnVideo2stFinnished()
    {
        ResetPuzzle();
        SetAs3st();
        rcs = roomClipState.sta3;
        InteractManager.instance.SetAble();
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

    private void SetAs2st()
    {
        vc1st.SetActive(false);
        for (int i = 0; i < SquareList.Count; i++)
        {
            if (isCorrectTarget2st[i])
            {
                SquareList[i].isCorrectSquare = true;
            }
        }
    }
    private void SetAs3st()
    {
        for (int i = 0; i < SquareList.Count; i++)
        {
            if (isCorrectTarget3st[i])
            {
                SquareList[i].isCorrectSquare = true;
            }
        }
    }

    protected override void OnDestroy()
    {
        //GlobalEventManager.instance.UnregisterEvent(receivedEventName, CheckAll);
        GlobalEventManager.instance.UnregisterEvent(onVideo1stEndName, OnVideo1stFinnished);
        GlobalEventManager.instance.UnregisterEvent(onVideo2stEndName, OnVideo2stFinnished);
    }
}
