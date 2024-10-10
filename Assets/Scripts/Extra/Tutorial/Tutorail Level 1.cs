using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorailLevel1 : Tutorial
{
    public List<string> lineNamesStartText;
    public List<string> lineNamesJack;
    public List<string> lineNamesAlex;
    public List<string> lineNamesEnd;

    private int linenumber = 0;
    private int tutorialsequence = 1;

    public GameObject Playfield;
    public GameObject CardOnly;

    private new void Update()
    {
        base.Update();

        switch (tutorialsequence)
        {
            case 1:
                Dialouge(lineNamesStartText); break;
            case 2:
                Stage2();
                break;
            case 3:
                HighlightObject(Playfield);
                break;
            case 4:
                Dialouge(lineNamesJack);
                break;
            case 5:
                HighlightObject(CardOnly);
                break;
            case 6: 
                Dialouge(lineNamesAlex); 
                break;
            case 7: 
                Stage7(); 
                break;
            case 8: 
                HighlightObject(UI.endTurn); 
                break;
            case 9: 
                if (Dialouge(lineNamesEnd)) levelManager.win.SetActive(true);
                break;
            default: 
                Debug.Log("no stage"); 
                break;
        }

    }

    void Start()
    {
        UI.Tutorial.SetActive(true);
        UI.deck.GetComponent<Button>().enabled = false;
    }
    // initial diaglouge played
    private bool Dialouge(List<string> lineNames)
    {
        UI.Tutorial.SetActive(true);
        if (isVoiceLinePlaying == true) return false;

        isVoiceLinePlaying = true;
        
        if (linenumber <= lineNames .Count - 1)
        {

            StartCoroutine(PlayVoiceLine(lineNames[linenumber]));
            linenumber++;

        }
        else
        {
            linenumber = 0;
            tutorialsequence ++;
            isVoiceLinePlaying = false;
            Debug.Log(tutorialsequence);
            UI.Tutorial.SetActive(false);
            return true;
        }
        return false;


    }
    // promptsz the user to select electrian
    private void Stage2()
    {
        Goal = playerManager.selectedCard;

        foreach(GameObject card in HandManager.hand)
        {
            if ((card.GetComponent<CardManager>().m_card.name) == "Electrician")
            {
                HighlightObject(card);
                break;
            }
        }

    }

    private void HighlightObject( GameObject objective)
    {
        if (Objective == Goal && coroutineplaying == true)
        {
            StopAllCoroutines();
            Goal = null;
            coroutineplaying = false;
            tutorialsequence ++;
            Debug.Log(tutorialsequence);
            return;
        }
        if (coroutineplaying == true) return;     
        handActive = true;
        coroutineplaying = true;
        Objective = objective;

        StartCoroutine(highlight());

    }
    private void Stage7()
    {
        Goal = playerManager.selectedCard;
        foreach (GameObject cards in HandManager.hand)
        {
            if ((cards.GetComponent<CardManager>().m_card.name) == "Plumber")
            {
                HighlightObject(cards);
                break;
            }
        }
    }
    private void Stage9()
    {
        
        
    }

    public new IEnumerator PlayVoiceLine(string name)
    {

        Tutoriallines T = Array.Find(lines, x => x.LineName == name);
        string text = T.Line;
        string voiceLine = T.AudioClipName;

        isVoiceLinePlaying = true;
        handActive = false;

        AudioManager.instance.PlaySFX(voiceLine);
        UI.TutorialActive(text);


        if (voiceLine != "") { yield return new WaitUntil(() => AudioManager.instance.sfxSource.isPlaying == false); }

        else { yield return new WaitForSeconds(5f); }

        EndText();

        yield break;

    }
    public new void Skip()
    {
        base.Skip();
        coroutineplaying=false;
    }
    

}
