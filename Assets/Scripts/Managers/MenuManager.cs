using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject fade;

    public void Awake()
    {
        GameManager.instance.Foreground = fade;
        StartCoroutine(GameManager.instance.TransitionIn());
    }
    public void changeScene(string LevelName)
    {
        GameManager.instance.ChangeScene(LevelName);
    }
    public void Quit()
    {
        GameManager.instance.Quit();
    }
}
