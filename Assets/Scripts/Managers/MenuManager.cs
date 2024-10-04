using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void changeScene(string LevelName)
    {
        GameManager.instance.ChangeScene(LevelName);
    }
    public void Quit()
    {
        GameManager.instance.Quit();
    }
}
