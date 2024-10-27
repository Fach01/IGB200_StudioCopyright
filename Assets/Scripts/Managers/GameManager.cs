using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int nextSceneLoad;
    public GameObject Foreground;
    public bool sceneloading = false;

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
        StartCoroutine(SceneChanger(sceneName));

    }
    public void ChangeScene(int sceneNum)
    {
        StartCoroutine(SceneChanger(sceneNum));
    }
    public IEnumerator TransitionIn()
    {
        sceneloading = true;
        Foreground.SetActive(true);
        Color c = Foreground.GetComponent<Image>().color;
        c.a = 1;
        Foreground.GetComponent<Image>().color = c;
        for (float i = 1f; i >= 0; i -= 0.1f)
        {
            c.a = i;
            Foreground.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.05f);
        }
        sceneloading = false;
        Foreground.SetActive(false);
    }
    public IEnumerator Transitionout()
    {
        sceneloading = true;
        AudioManager.instance.PlaySFX("Change Scene");
        Foreground.SetActive(true);
        Color c = Foreground.GetComponent<Image>().color;
        c.a = 0;
        Foreground.GetComponent<Image>().color = c;

        for (float i = 0f; i <= 1; i += 0.1f)
        {
            c.a = i;
            Foreground.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.05f);
        }
        sceneloading = false;
    }
    // for use in case of just switching panels
    public IEnumerator TransitionInOut()
    {
        StartCoroutine(Transitionout());
        yield return new WaitUntil(() => sceneloading == false);
        StartCoroutine(TransitionIn());
    }
    public void InThenOut()
    {
        StartCoroutine(TransitionInOut());
    }
    public void Intro()
    {
        StartCoroutine(TransitionIn());
    }
    public void Outro()
    {
        StartCoroutine(Transitionout());
    }
    /// <summary>
    /// Plays transition then changes the scene
    /// </summary>
    /// <param name="sceneName"> Name of the scene you are going to</param>
    /// <returns></returns>
    public IEnumerator SceneChanger(string sceneName)
    {
        StartCoroutine(Transitionout());
        yield return new WaitUntil(() => sceneloading == false);
        try
        {
            SceneManager.LoadScene(sceneName);
        }
        catch
        {
            Debug.Log("Check Scene name is correct and in build");
        }

    }

    public IEnumerator SceneChanger(int sceneNum)
    {
        StartCoroutine(Transitionout());
        yield return new WaitUntil(() => sceneloading == false);
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
        ChangeScene("Level Selector");
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
