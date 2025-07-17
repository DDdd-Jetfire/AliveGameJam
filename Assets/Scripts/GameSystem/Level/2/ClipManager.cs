using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipManager : MonoBehaviour
{

    public List<Square> SquareList = new List<Square>();

    public List<bool> isCorrectTarget1st = new List<bool>();


    public string nextScene;

    public AudioPlayer ap;
    protected bool levelState = true;

    protected virtual void Start()
    {

    }

    protected virtual void CheckAll()
    {

    }

    protected virtual void LevelSetFault()
    {
        levelState = false;
    }

    protected virtual void LevelCa()
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


    protected virtual void OnVideoFinnished()
    {

    }


    public void NextButtonDown()
    {
        CheckAll();
    }

    protected virtual void SetAs1st()
    {
        for (int i = 0; i < SquareList.Count; i++)
        {
            if (isCorrectTarget1st[i])
            {
                SquareList[i].isCorrectSquare = true;
            }
        }
    }

    protected virtual void ResetPuzzle()
    {
        for (int i = 0; i < SquareList.Count; i++)
        {
            SquareList[i].ResetPuzzle();
        }
    }

    protected virtual void OnDestroy()
    {

    }
}
