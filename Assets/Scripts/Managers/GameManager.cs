using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int nextSceneLoad;

    public static GameManager instance;
    public void Start()
    {

    }
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }
    public void UnlockNextLevel() 
    {
        PlayerPrefs.SetInt("LevelAt", nextSceneLoad);
    }
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
    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
  
    }
    public void Quit()
    {
        Application.Quit();
    }
}
