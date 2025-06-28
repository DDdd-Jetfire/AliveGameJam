using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // ����ʵ��
    public static SceneController instance;

    [Header("��������")]
    [SerializeField] private GameObject loadingScreen; // ��ѡ������UI����
    [SerializeField] private float minLoadTime = 1f;   // ��ѡ����С����ʱ�䣨����������

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ͨ�����Ƽ��س���
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // �˳���Ϸ
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
