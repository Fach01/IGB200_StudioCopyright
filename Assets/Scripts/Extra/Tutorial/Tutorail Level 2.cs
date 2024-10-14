using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorailLevel2 : Tutorial
{
    public List<string> startLines;
    public List<string> farsightLines;
    public List<string> foundationLines;
    public List<string> drawLines;

    private int linenumber = 0;
    private bool tutorialstartplayed = false;
    private bool drawtutplayed = false;
    private bool line1played = false;
    private bool line2played = false;

    private new void Update()
    {
        base.Update();
        if (!tutorialstartplayed) StartTutorial();
        if (playerManager.selectedCard != null)
        {
            string ability = playerManager.selectedCard.GetComponent<CardManager>().m_card.abilityName;
            if (ability == "Farsight" && !line1played) Farsight();
            else if (ability == "Foundation" && !line2played) Foundation();
        }
        if (playerManager.phase == Phase.Setup && levelManager.turn != 1)
        {
            Draw();
        }
    }
    private void Dialouge(List<string> lineNames, ref bool p)
    {
        UI.Tutorial.SetActive(true);
        if (isVoiceLinePlaying == true) return;

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
        }


    }
    void Awake()
    {
        UI.Tutorial.SetActive(true);
        UI.deck.GetComponent<Button>().enabled = false;
    }
    private void StartTutorial()
    {
        Dialouge(startLines, ref tutorialstartplayed);
    }
    public void Farsight()
    {
        Dialouge(farsightLines, ref line1played);
    }
    public void Foundation()
    {
        Dialouge(foundationLines, ref line2played);
    }
    public void Draw()
    {
        Dialouge(drawLines, ref drawtutplayed);

    }
    public new void Skip()
    {
        base.Skip();
        coroutineplaying = false;
    }

}
