using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonClipManager : MonoBehaviour
{
    public List<LemonSquare> lemonSquareList = new List<LemonSquare>();
    public List<bool> isCorrectTarget1st = new List<bool>();


    public string receivedEventName = "TriggerBikeButton";
    public UnityEditor.SceneAsset nextScene;

    void Start()
    {
        LemonSquare[] lc = gameObject.transform.GetComponentsInChildren<LemonSquare>();
        foreach (var l in lc)
        {
            lemonSquareList.Add(l);
        }
        SetAs1st();
    }
    void CheckAll()
    {
        bool isAllSelect = true;
        for (int i = 0; i < lemonSquareList.Count; i++)
        {
            if (isCorrectTarget1st[i] != lemonSquareList[i].isSelect)
            {
                lemonSquareList[i].SetFalse();
                isAllSelect = false;
            }
        }
        if (isAllSelect)
        {
            GameManager.instance.GoToNextScene(nextScene.name);
        }
    }


    public void NextButtonDown()
    {
        CheckAll();
    }

    private void SetAs1st()
    {
        for (int i = 0; i < lemonSquareList.Count; i++)
        {
            if (isCorrectTarget1st[i])
            {
                lemonSquareList[i].isCorrectSquare = true;
            }
        }
    }


    private void OnDestroy()
    {
        //GlobalEventManager.instance.UnregisterEvent(receivedEventName, CheckAll);
        //GlobalEventManager.instance.UnregisterEvent(onVideoEventName, OnVideoFinnished);
    }
}
