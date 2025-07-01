using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonClipManager : MonoBehaviour
{
    public List<LemonSquare> lemonSquareList = new List<LemonSquare>();
    public List<bool> isCorrectTarget1st = new List<bool>();


    public string receivedEventName = "TriggerBikeButton";
    public string nextScene;

    private bool levelState = true; 
    public AudioPlayer ap;
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
        if (!InteractManager.instance.canInteract)
        {
            return;
        }

        InteractManager.instance.SetDisable();
        bool isAllSelect = true;
        for (int i = 0; i < lemonSquareList.Count; i++)
        {
            if (isCorrectTarget1st[i])
            {
                if (!lemonSquareList[i].isSelect)
                {
                    lemonSquareList[i].SetFalse();
                    isAllSelect = false;
                }
            }
            else
            {
                if (lemonSquareList[i].isSelect)
                {
                    lemonSquareList[i].SetFalse();
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
    private void LevelSetFault()
    {
        levelState = false;
    }

    private void LevelCa()
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
