using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonSquare : InteractBase
{
    public bool isCorrectSquare = false;
    public bool isSelect = false;

    public GameObject correct;
    public GameObject fault;

    public VideoController vc;

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
            correct.SetActive(true);
            vc.PlayVideo();
            //if (isCorrectSquare)
            //{
            //    correct.SetActive(true);
            //}
            //else
            //{
            //    fault.SetActive(true);
            //}
        }
        else
        {
            correct.SetActive(false);
            fault.SetActive(false);
        }
        base.Click();
    }

    public void SetFalse()
    {
        isSelect = true;
        correct.SetActive(false);
        fault.SetActive(true);
    }

    public void ResetPuzzle()
    {
        isSelect = false;
        isCorrectSquare = false;
        correct.SetActive(false);
        fault.SetActive(false);
    }

    private void OnDestroy()
    {

    }
}
