using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonSquare : Square
{

    //public VideoController vc;
    public NVC nvc;

    public bool isPlayed = false;


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
    private void OnDestroy()
    {

    }
}
