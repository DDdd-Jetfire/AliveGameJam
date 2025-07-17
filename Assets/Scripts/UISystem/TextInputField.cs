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

    // �ύʱ��֤
    private void OnSubmitInput(string text)
    {
        if (CheckValidity(text))
        {
            Debug.Log($"�ύ��Ч����: {text}");

        }
    }

    // ��֤����
    private bool CheckValidity(string input)
    {
        return true;
    }
}
