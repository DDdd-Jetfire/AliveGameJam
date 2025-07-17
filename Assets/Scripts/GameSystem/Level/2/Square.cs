using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : InteractBase
{
    public bool isCorrectSquare = false;
    public bool isSelect = false;

    public GameObject correct;
    public GameObject fault;


    public SpriteRenderer spr;
    public Sprite unSelect;
    public Sprite inSelect;

    protected virtual void Start()
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
