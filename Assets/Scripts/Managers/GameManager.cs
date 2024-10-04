using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        try
        {
            SceneManager.LoadScene(sceneName);
        }
        catch
        {
            Debug.Log("Check Scene name is correct and in build");
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
