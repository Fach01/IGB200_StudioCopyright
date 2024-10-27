using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject fade;
    public GameObject quitButton;

    public void Awake()
    {
        GameManager.instance.Foreground = fade;
        StartCoroutine(GameManager.instance.TransitionIn());

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            // Hide the quit button for WebGL builds
            quitButton.SetActive(false);
        }
    }
    public void changeScene(string LevelName)
    {
        GameManager.instance.ChangeScene(LevelName);
    }
    public void playaudio()
    {
        AudioManager.instance.PlaySFX("Select Option");
    }
    public void Quit()
    {
        GameManager.instance.Quit();
    }
    public void TransitionInOut()
    {
        GameManager.instance.InThenOut();
    }
    public void Active(GameObject Panel)
    {
        StartCoroutine(setActive(Panel));
    }
    public void InActive(GameObject Panel)
    {
        StartCoroutine(SetInActive(Panel));
    }
    public IEnumerator setActive(GameObject panel)
    {
        yield return new WaitForSeconds(0.5f);
        panel.SetActive(!panel.activeInHierarchy); 
    }
    public IEnumerator SetInActive(GameObject panel)
    {
        yield return new WaitForSeconds(0.5f);
        panel.SetActive(false);
    }

}
