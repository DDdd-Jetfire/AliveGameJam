using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeSquare : Square
{

    public override void Click()
    {
        isSelect = !isSelect;
        //GlobalEventManager.instance.TriggerEvent("TriggerBikeButton");
        if (isSelect)
        {
            correct.SetActive(true);
            spr.sprite = inSelect;
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
}
