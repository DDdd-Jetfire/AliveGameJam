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
    private float levelChangeParameter = 0.25f;

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
        float changedValue = 0;
        changedValue = levelChangeParameter * 100 * (1 - Mathf.Abs(GameManager.instance.humanPoint * 0.01f - 0.5f) / 0.5f);
        Debug.Log($"changedValue:{changedValue}");
        if (levelState)
        {
            GameManager.instance.AddHumanValue(changedValue);
        }
        else
        {
            GameManager.instance.AddHumanValue(changedValue* -1);
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
