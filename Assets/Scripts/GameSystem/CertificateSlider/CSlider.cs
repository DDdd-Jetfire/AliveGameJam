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
        sl.value = tempValue / 100;
        tmp.text = $"<b>{Mathf.FloorToInt(sl.value * 100)}%</b>";
    }

    private void OnDestroy()
    {
        
    }
}
