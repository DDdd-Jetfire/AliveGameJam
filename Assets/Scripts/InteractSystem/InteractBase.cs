using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBase : MonoBehaviour,IInteractable
{

    public string clickEventName = "null";

    void Start()
    {
        
    }

    void Update()
    {

    }
    public virtual void Click()
    {
        Debug.Log("click");
        if (clickEventName != "null")
        {
            GlobalEventManager.instance.TriggerEvent(clickEventName);
        }
    }
}
