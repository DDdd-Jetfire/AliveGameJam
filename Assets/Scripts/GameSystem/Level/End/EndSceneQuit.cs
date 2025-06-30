using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneQuit : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.QuitGame();
        }
    }
}
