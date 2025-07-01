using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float originHumanPoint = 0;
    public float humanPoint = 0;

    public float maxHumanPoint = 125;
    public float minHumanPoint = -25;

    private void Awake()
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
        originHumanPoint = humanPoint;
    }

    void Start()
    {
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
        CheckHumanPoint();
    }
    public void AddHumanValue(float value)
    {
        humanPoint += value;
        CheckHumanPoint();
    }

    private void CheckHumanPoint()
    {
        if (humanPoint < minHumanPoint) humanPoint = minHumanPoint;
        if (humanPoint > maxHumanPoint) humanPoint = maxHumanPoint;
    }

    IEnumerator SettlementIE(string sceneName)
    {

        float elapse = 0;
        float maxFinnishTime = 2f;
        Debug.Log($"humanPoint{humanPoint}and originHumanPoint{originHumanPoint}");
        if(humanPoint == originHumanPoint)
        {
            elapse = maxFinnishTime;
            Debug.Log("jump");
        }

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

        elapse = 0;

        float maxWaitTime = 1f;

        while (elapse < maxWaitTime)
        {
            elapse += Time.deltaTime;
            yield return null;
        }
        SceneController.instance.LoadScene(sceneName);
    }
}
