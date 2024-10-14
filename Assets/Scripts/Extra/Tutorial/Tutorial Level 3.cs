using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel3 : Tutorial
{
    public List<string> startLines;
    public List<string> eventLines;
    public List<string> endLines;

    public bool levelWon = false;
    private int linenumber = 0;
    private bool startplayed = false;
    private bool endplayed = false;
    private bool playsickday = false;
    private bool sickdayplayed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public new void Update()
    {
        base.Update();

        StartTutorial();
        playsickday = levelManager.tutorialplayed;
        levelWon = levelManager.levelWon;
        if(playsickday)
        {
            SickDay();
        }
        
        if (levelWon)
        {
            End();
        }
       
    }
    private void StartTutorial()
    {
        if (startplayed) return;
        Dialouge(startLines, ref startplayed);
    }
    private bool Dialouge(List<string> lineNames, ref bool p)
    {
        UI.Tutorial.SetActive(true);
        if (isVoiceLinePlaying == true) return false;

        isVoiceLinePlaying = true;

        if (linenumber <= lineNames.Count - 1)
        {

            StartCoroutine(PlayVoiceLine(lineNames[linenumber]));
            linenumber++;

        }
        else
        {
            linenumber = 0;
            p = true;
            isVoiceLinePlaying = false;
            UI.Tutorial.SetActive(false);
            return true;
        }
        return false;
    }
    public void SickDay()
    {
        if (sickdayplayed) return;
        Dialouge(eventLines, ref sickdayplayed);

    }
    public void End()
    {
        if(Dialouge(endLines, ref endplayed)) levelManager.win.SetActive(true);

    }

}
