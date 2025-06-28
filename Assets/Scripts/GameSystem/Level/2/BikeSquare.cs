using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeSquare : InteractBase
{

    public enum SelectState
    {
        normal,
        correct,
        fault,
    }

    public bool isCorrectSquare = false;
    public bool isSelect = false;

    public GameObject correct;
    public GameObject fault;


    private void Start()
    {
        if (correct == null || fault == null)
        {
            Debug.Log("missing object");
            return;
        }
        correct.SetActive(false);
        fault.SetActive(false);
    }

    public override void Click()
    {
        isSelect = !isSelect;
        //GlobalEventManager.instance.TriggerEvent("TriggerBikeButton");
        if (isSelect)
        {
            if (isCorrectSquare)
            {
                correct.SetActive(true);
            }
            else
            {
                fault.SetActive(true);
            }
        }
        else
        {
            correct.SetActive(false);
            fault.SetActive(false);
        }
        base.Click();
    }

    public void ResetPuzzle()
    {
        isSelect = false;
        correct.SetActive(false);
        fault.SetActive(false);
    }

    private void OnDestroy()
    {
        
    }
}
