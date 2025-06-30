using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class RoomClipManager : MonoBehaviour
{
    public List<BikeSquare> roomSquareList = new List<BikeSquare>();

    public List<bool> isCorrectTarget1st = new List<bool>();
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
    public string nextScene;
    public TextMeshProUGUI tmpMessage;

    public GameObject vc1st;

    public GameObject tmpCanvas1st;
    public GameObject tmpCanvas2st;

    public AudioPlayer ap;
    void Start()
    {
        //GlobalEventManager.instance.RegisterEvent(receivedEventName, CheckAll);
        GlobalEventManager.instance.RegisterEvent(onVideo1stEndName, OnVideo1stFinnished);
        GlobalEventManager.instance.RegisterEvent(onVideo2stEndName, OnVideo2stFinnished);
        BikeSquare[] rs = gameObject.transform.GetComponentsInChildren<BikeSquare>();
        foreach (var r in rs)
        {
            roomSquareList.Add(r);
        }
        SetAs1st();
        tmpCanvas1st.SetActive(true);
        tmpCanvas2st.SetActive(false);
    }
    void CheckAll()
    {
        switch (rcs)
        {
            case roomClipState.sta1:
                bool isAllSelect = true;
                for (int i = 0; i < roomSquareList.Count; i++)
                {
                    if (isCorrectTarget1st[i])
                    {
                        if (!roomSquareList[i].isSelect)
                        {
                            roomSquareList[i].SetFalse();
                            isAllSelect = false;
                        }
                    }
                    else
                    {
                        if (roomSquareList[i].isSelect)
                        {
                            roomSquareList[i].SetFalse();
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

                    ap.PlaySoundEffects(1);
                }
                break;
            case roomClipState.sta2:

                bool isAllSelect2 = true;
                for (int i = 0; i < roomSquareList.Count; i++)
                {
                    if (isCorrectTarget2st[i])
                    {
                        if (!roomSquareList[i].isSelect)
                        {
                            roomSquareList[i].SetFalse();
                            isAllSelect2 = false;
                        }
                    }
                    else
                    {
                        if (roomSquareList[i].isSelect)
                        {
                            roomSquareList[i].SetFalse();
                        }
                    }
                }
                if (isAllSelect2)
                {
                    GlobalEventManager.instance.TriggerEvent(vc2stEnd);
                    ap.PlaySoundEffects(1);
                }
                else
                {

                    ap.PlaySoundEffects(0);
                }
                break;
            case roomClipState.sta3:

                bool isAllSelect3 = true;
                for (int i = 0; i < roomSquareList.Count; i++)
                {
                    if (isCorrectTarget3st[i])
                    {
                        if (!roomSquareList[i].isSelect)
                        {
                            roomSquareList[i].SetFalse();
                            //isAllSelect2 = false;
                        }
                    }
                    else
                    {
                        if (roomSquareList[i].isSelect)
                        {
                            roomSquareList[i].SetFalse();
                        }
                    }
                }
                if (isAllSelect3)
                {
                    //GlobalEventManager.instance.TriggerEvent(finnishEventName);
                    GameManager.instance.GoToNextScene(nextScene);

                    ap.PlaySoundEffects(0);
                }
                else
                {
                    GameManager.instance.GoToNextScene(nextScene);
                    ap.PlaySoundEffects(1);
                }
                break;
        }
    }

    void OnVideo1stFinnished()
    {
        ResetPuzzle();
        SetAs2st();
        rcs = roomClipState.sta2;

        tmpCanvas1st.SetActive(false);
        tmpCanvas2st.SetActive(true);
    }
    void OnVideo2stFinnished()
    {
        ResetPuzzle();
        SetAs3st();
        rcs = roomClipState.sta3;
    }


    public void NextButtonDown()
    {
        CheckAll();
    }

    private void SetAs1st()
    {
        for (int i = 0; i < roomSquareList.Count; i++)
        {
            if (isCorrectTarget1st[i])
            {
                roomSquareList[i].isCorrectSquare = true;
            }
        }
    }

    private void ResetPuzzle()
    {
        for (int i = 0; i < roomSquareList.Count; i++)
        {
            roomSquareList[i].ResetPuzzle();
        }
    }
    private void SetAs2st()
    {
        vc1st.SetActive(false);
        for (int i = 0; i < roomSquareList.Count; i++)
        {
            if (isCorrectTarget2st[i])
            {
                roomSquareList[i].isCorrectSquare = true;
            }
        }
    }
    private void SetAs3st()
    {
        for (int i = 0; i < roomSquareList.Count; i++)
        {
            if (isCorrectTarget3st[i])
            {
                roomSquareList[i].isCorrectSquare = true;
            }
        }
    }

    private void OnDestroy()
    {
        //GlobalEventManager.instance.UnregisterEvent(receivedEventName, CheckAll);
        GlobalEventManager.instance.UnregisterEvent(onVideo1stEndName, OnVideo1stFinnished);
        GlobalEventManager.instance.UnregisterEvent(onVideo2stEndName, OnVideo2stFinnished);
    }
}
