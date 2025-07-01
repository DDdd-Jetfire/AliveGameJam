using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CSlider : MonoBehaviour
{

    public Slider sl;
    public TextMeshProUGUI tmp;

    public string onUpdateValue = "ValueUpdate";

    void Start()
    {
        GlobalEventManager.instance.RegisterEvent(onUpdateValue, UpdateValue);
        UpdateValue();
    }

    void Update()
    {
        
    }

    private void UpdateValue()
    {
        float tempValue = GameManager.instance.humanPoint;
        //Debug.Log($"tempValue1 is {tempValue}");
        tempValue /= 100;
        if (tempValue < 0) tempValue = 0;
        if (tempValue > 1) tempValue = 1;
        //Debug.Log($"tempValue2 is {tempValue}");
        sl.value = tempValue;
        tmp.text = $"<b>{Mathf.FloorToInt(sl.value * 100)}%</b>";
    }

    private void OnDestroy()
    {
        
    }
}
