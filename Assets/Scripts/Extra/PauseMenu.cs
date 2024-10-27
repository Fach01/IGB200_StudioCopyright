using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject gameManager;
    public void OnResume()
    {
        this.gameObject.SetActive(false);
    }

    public void OnRestart()
    {
        gameManager.GetComponent<GameManager>().Restart();
    }

    public void OnMenu()
    {
        gameManager.GetComponent<GameManager>().ReturntoMain();
    }
}
