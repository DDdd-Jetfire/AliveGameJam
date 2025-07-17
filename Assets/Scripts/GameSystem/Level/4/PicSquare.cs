using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicSquare : Square
{

    public AudioPlayer ap;

    public override void Click()
    {
        isSelect = !isSelect;
        if (isSelect)
        {
            correct.SetActive(true);
            if (inSelect != null)
            {
                spr.sprite = inSelect;
            }
            if (ap != null)
            {
                ap.PlaySoundEffects(0);
            }
            //playAudioHere;
            
        }
        else
        {
            if (unSelect != null)
            {
                spr.sprite = unSelect;
            }
            correct.SetActive(false);
            fault.SetActive(false);
        }
        base.Click();
    }

}
