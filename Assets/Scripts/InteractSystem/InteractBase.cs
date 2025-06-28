using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBase : MonoBehaviour,IInteractable
{

    void Start()
    {
        
    }

    void Update()
    {

    }
    public virtual void Click()
    {
        Debug.Log("base click");
    }
}
