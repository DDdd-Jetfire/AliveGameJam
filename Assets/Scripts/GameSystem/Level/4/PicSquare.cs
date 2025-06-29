using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicSquare : InteractBase
{
    public bool isCorrectSquare = false;
    public bool isSelect = false;

    public GameObject correct;
    public GameObject fault;


    public SpriteRenderer spr;
    public Sprite unSelect;
    public Sprite inSelect;

    public AudioPlayer ap;

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

    public void SetFalse()
    {
        isSelect = true;
        correct.SetActive(false);
        fault.SetActive(true);
    }

    public void ResetPuzzle()
    {
        isSelect = false;
        if (unSelect != null)
        {
            spr.sprite = unSelect;
        }
        isCorrectSquare = false;
        correct.SetActive(false);
        fault.SetActive(false);
    }

    private void OnDestroy()
    {

    }
}
