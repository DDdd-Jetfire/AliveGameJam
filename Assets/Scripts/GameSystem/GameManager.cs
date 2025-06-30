using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float originHumanPoint = 0;
    public float humanPoint = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        originHumanPoint = humanPoint;
    }
    void Update()
    {
        
    }

    public void GoToNextScene(string sceneName)
    {
        StartCoroutine(SettlementIE(sceneName));

    }

    public void QuitGame()
    {
        SceneController.instance.QuitGame();
    }

    public void UpdateHumanValue(float value)
    {
        humanPoint = value;
    }

    IEnumerator SettlementIE(string sceneName)
    {
        if (originHumanPoint == humanPoint)
        {

        }
        float elapse = 0;
        float maxFinnishTime = 2f;
        float targetPoint = humanPoint;
        humanPoint = originHumanPoint;
        while (elapse < maxFinnishTime)
        {
            elapse += Time.deltaTime;
            humanPoint = Mathf.Lerp(originHumanPoint, targetPoint, elapse / maxFinnishTime);
            GlobalEventManager.instance.TriggerEvent("ValueUpdate");
            yield return null;
        }
        originHumanPoint = targetPoint;


        float maxWaitTime = 1f;

        while (elapse < maxWaitTime)
        {
            elapse += Time.deltaTime;
            yield return null;
        }
        SceneController.instance.LoadScene(sceneName);
    }
}
