using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicClipManager : MonoBehaviour
{
    public List<PicSquare> picSquareList = new List<PicSquare>();
    public List<bool> isCorrectTarget1st = new List<bool>();


    public string receivedEventName = "TriggerBikeButton";
    public string nextScene;

    void Start()
    {
        PicSquare[] lc = gameObject.transform.GetComponentsInChildren<PicSquare>();
        foreach (var l in lc)
        {
            picSquareList.Add(l);
        }
        SetAs1st();
    }
    void CheckAll()
    {
        //bool isAllSelect = true;
        for (int i = 0; i < picSquareList.Count; i++)
        {
            if (isCorrectTarget1st[i])
            {
                if (!picSquareList[i].isSelect)
                {
                    picSquareList[i].SetFalse();
                    //isAllSelect2 = false;
                }
            }
            else
            {
                if (picSquareList[i].isSelect)
                {
                    picSquareList[i].SetFalse();
                }
            }
        }
        //if (isAllSelect)
        //{
        //    GameManager.instance.GoToNextScene(nextScene.name);
        //}
        GameManager.instance.GoToNextScene(nextScene);
    }


    public void NextButtonDown()
    {
        CheckAll();
    }

    private void SetAs1st()
    {
        for (int i = 0; i < picSquareList.Count; i++)
        {
            if (isCorrectTarget1st[i])
            {
                picSquareList[i].isCorrectSquare = true;
            }
        }
    }


    private void OnDestroy()
    {
        //GlobalEventManager.instance.UnregisterEvent(receivedEventName, CheckAll);
        //GlobalEventManager.instance.UnregisterEvent(onVideoEventName, OnVideoFinnished);
    }
}
