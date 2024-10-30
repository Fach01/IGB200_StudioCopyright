using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Linq;

public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons;
    public int[] buildingbuttons;
    public GameObject selectButton;
    // Start is called before the first frame update
    void Start()
    {
        int LevelAt = PlayerPrefs.GetInt("LevelAt", 2 );
        Debug.Log(LevelAt);
        for(int i = 0; i < lvlButtons.Length; i++)
        {
            if(buildingbuttons.Contains(i) && (i + 2) <= LevelAt)
            {
                LevelAt += 1;   
            }
            if ((i + 2) > LevelAt)
            {
                lvlButtons[i].interactable = false;
            }
            else
            {
                
                lvlButtons[i].interactable = true;
            }
                
        }
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) { PlayerPrefs.DeleteAll(); } // VERY IMPORTANT DELETE BEFORE BUILDING THIS WAS USED IN BUG TESTING 
    }
}
