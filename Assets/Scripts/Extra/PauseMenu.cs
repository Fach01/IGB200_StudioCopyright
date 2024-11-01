using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void OnResume()
    {
        this.gameObject.SetActive(false);
    }

    public void OnRestart()
    {
        GameManager.instance.Restart();
    }

    public void OnMenu()
    {
        GameManager.instance.ReturntoMain();
    }
}
