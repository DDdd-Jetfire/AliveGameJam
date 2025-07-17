using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextInputField : MonoBehaviour
{
    private TMP_InputField inputF;

    void Start()
    {
        inputF = gameObject.GetComponent<TMP_InputField>();

        inputF.onEndEdit.AddListener(OnSubmitInput);
    }

    void Update()
    {

    }

    // 提交时验证
    private void OnSubmitInput(string text)
    {
        if (CheckValidity(text))
        {
            Debug.Log($"提交有效输入: {text}");

        }
    }

    // 验证方法
    private bool CheckValidity(string input)
    {
        return true;
    }
}
