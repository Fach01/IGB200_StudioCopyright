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
        int LevelAt = PlayerPrefs.GetInt("LevelAt", 3);

        for(int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 3 > LevelAt)
                lvlButtons[i].interactable = false;
        }
    }
}
