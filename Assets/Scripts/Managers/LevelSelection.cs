using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons;
    // Start is called before the first frame update
    void Start()
    {
        int LevelAt = PlayerPrefs.GetInt("LevelAt", 4);
        Debug.Log(LevelAt);
        for(int i = 0; i < lvlButtons.Length; i++)
        {
            if ((i + 4) > LevelAt)
            {
                lvlButtons[i].interactable = false;
            }
                
        }
    }
}
