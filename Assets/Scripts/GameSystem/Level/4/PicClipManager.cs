using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicClipManager : MonoBehaviour
{
    public List<PicSquare> picSquareList = new List<PicSquare>();
    public List<bool> isCorrectTarget1st = new List<bool>();


    public string receivedEventName = "TriggerBikeButton";
    public string nextScene;

    public AudioPlayer ap;

    private bool levelState = true;
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
        if (!InteractManager.instance.canInteract)
        {
            return;
        }

        InteractManager.instance.SetDisable();
        bool isAllSelect = true;
        for (int i = 0; i < picSquareList.Count; i++)
        {
            if (isCorrectTarget1st[i])
            {
                if (!picSquareList[i].isSelect)
                {
                    picSquareList[i].SetFalse();
                    isAllSelect = false;
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
