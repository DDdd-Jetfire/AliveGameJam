using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonSquare : InteractBase
{
    public bool isCorrectSquare = false;
    public bool isSelect = false;

    public GameObject correct;
    public GameObject fault;

    //public VideoController vc;
    public NVC nvc;

    public bool isPlayed = false;

    public SpriteRenderer spr;
    public Sprite unSelect;
    public Sprite inSelect;

    private void Start()
    {
        if (correct == null || fault == null)
        {
            Debug.Log("missing object");
            return;
        }
        spr = gameObject.GetComponent<SpriteRenderer>();
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
            spr.sprite = inSelect;

            if (!isPlayed)
            {
                if (nvc != null)
                {
                    nvc.PlayVideo();
                }
                isPlayed = true;
            }
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
            spr.sprite = unSelect;
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
        spr.sprite = unSelect;
        isCorrectSquare = false;
        correct.SetActive(false);
        fault.SetActive(false);
    }

    private void OnDestroy()
    {

    }
}
