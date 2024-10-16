using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int nextSceneLoad;
    public GameObject Foreground;

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
        if (nextSceneLoad > PlayerPrefs.GetInt("LevelAt"))
        {
            PlayerPrefs.SetInt("LevelAt", nextSceneLoad);
        }
        
    }
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(Transitionout(sceneName));

    }
    public IEnumerator TransitionIn()
    {
        Foreground.SetActive(true);
        Color c = Foreground.GetComponent<Image>().color;
        c.a = 1;
        Foreground.GetComponent<Image>().color = c;
        for (float i = 1f; i >= 0; i -= 0.1f)
        {
            c.a = i;
            Foreground.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.1f);
        }
        Foreground.SetActive(false);
    }
    public IEnumerator Transitionout(string sceneName)
    {
        Foreground.SetActive(true);     
        Color c = Foreground.GetComponent<Image>().color;
        c.a = 0;
        Foreground.GetComponent<Image>().color = c;

        for (float i = 0f; i <=1; i += 0.1f)
        {
            c.a = i;
            Foreground.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.1f);
        }
        try
        {
            SceneManager.LoadScene(sceneName);
        }
        catch
        {
            Debug.Log("Check Scene name is correct and in build");
        }

    }
    public IEnumerator Transitionout(int sceneNum)
    {
        AudioManager.instance.PlaySFX("Change Scene");
        Foreground.SetActive(true);
        Color c = Foreground.GetComponent<Image>().color;
        c.a = 0;
        Foreground.GetComponent<Image>().color = c;

        for (float i = 0f; i <= 1; i += 0.1f)
        {
            c.a = i;
            Foreground.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.1f);
        }
        try
        {
            SceneManager.LoadScene(sceneNum);
        }
        catch
        {
            Debug.Log("Check Scene name is correct and in build");
        }

    }
    public void ReturntoMain()
    {
        ChangeScene("Main Menu");
    }
    public void ChangeScene(int sceneNumber)
    {
        StartCoroutine(Transitionout(sceneNumber));

    }
    public void NextLevel()
    {
        int Scenenum = SceneManager.GetActiveScene().buildIndex + 1;
        ChangeScene(Scenenum);
    }
    public void Restart()
    {
        int Scenenum = SceneManager.GetActiveScene().buildIndex;
        ChangeScene(Scenenum);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
