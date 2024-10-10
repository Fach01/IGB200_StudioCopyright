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
                StartTutorial(); break;
            case 2:
                Stage2();
                break;
            case 3:
                Stage3();
                break;
            case 4:
                Stage4();
                break;
            case 5:
                Stage5();
                break;
            case 6: Stage3(); break;
            case 7: Stage6(); break;
            case 8: Stage7(); break;
            case 9: ; break;
        }

    }

    void Start()
    {
        UI.Tutorial.SetActive(true);
        UI.deck.GetComponent<Button>().enabled = false;
        Invoke("StartTutorial", 0.4f);
    }
    private void StartTutorial()
    {
        if (isVoiceLinePlaying == true) return;

        isVoiceLinePlaying = true;
        
        if (linenumber <= lineNamesStartText.Count - 1)
        {
            Debug.Log(linenumber);

            StartCoroutine(PlayVoiceLine(lineNamesStartText[linenumber]));
            linenumber++;

        }
        else
        {
            tutorialsequence = 2;
            UI.Tutorial.SetActive(false); 
        }


    }
    
    private void Stage2()
    {
        Goal = playerManager.selectedCard;
        if (Objective == Goal && coroutineplaying == true)
        {
            StopAllCoroutines();
            Goal = null;
            coroutineplaying = false;
            tutorialsequence = 3;
            return;
        }
        if (coroutineplaying == true) return;

        coroutineplaying = true;
        handActive = true;
        foreach(GameObject card in HandManager.hand)
        {
            if ((card.GetComponent<CardManager>().m_card.name) == "Electrician") Objective = card;
        }
        StartCoroutine(highlight());

    }

    private void Stage3()
    {
        if (Objective == Goal && coroutineplaying == true)
        {
            StopAllCoroutines();
            Goal = null;
            coroutineplaying = false;
            tutorialsequence ++;
            return;
        }
        if (coroutineplaying == true) return;     
        handActive = true;
        coroutineplaying = true;
        Objective = Playfield;

        StartCoroutine(highlight());

    }
    private void Stage4()
    {
        if (!coroutineplaying) linenumber = 0;

        if(!isVoiceLinePlaying && linenumber < lineNamesJack.Count) 
        {
            isVoiceLinePlaying = true;

            StartCoroutine(PlayVoiceLine(lineNamesJack[linenumber])); 
            linenumber++; 
        }
        else if (!isVoiceLinePlaying && linenumber >= lineNamesJack.Count) { UI.Tutorial.SetActive(false); }

        if (Objective == Goal && coroutineplaying)
        {
            StopAllCoroutines();
            Goal = null;
            coroutineplaying = false;
            tutorialsequence = 5;
            UI.Tutorial.SetActive(false);
            return;
        }
        if (coroutineplaying) return;
        UI.Tutorial.SetActive(true);
        coroutineplaying = true;
        Objective = CardOnly;


        StartCoroutine(highlight());
    }
    private void Stage5()
    {
        Goal = playerManager.selectedCard;
        if (!coroutineplaying) linenumber = 0;

        if (!isVoiceLinePlaying && linenumber < lineNamesAlex.Count) 
        { 
            isVoiceLinePlaying = true;
            StartCoroutine(PlayVoiceLine(lineNamesAlex[linenumber])); 
            linenumber++; 
        }
        else if (!isVoiceLinePlaying && linenumber >= lineNamesAlex.Count) { UI.Tutorial.SetActive(false); }

        if (Objective == Goal && coroutineplaying)
        {
            StopAllCoroutines();
            Goal = null;
            coroutineplaying = false;
            tutorialsequence = 6;
            UI.Tutorial.SetActive(false);
            return;
        }
        if (coroutineplaying) return;
        UI.Tutorial.SetActive(true);

        coroutineplaying = true;

        foreach (GameObject card in HandManager.hand)
        {
            if ((card.GetComponent<CardManager>().m_card.name) == "Plumber") Objective = card;
        }

        StartCoroutine(highlight());
    }
    private void Stage6()
    {

        if (Objective == Goal && coroutineplaying == true)
        {
            StopAllCoroutines();
            Goal = null;
            coroutineplaying = false;
            tutorialsequence = 8;
            return;
        }
        if (coroutineplaying == true) return;
        coroutineplaying = true;
        handActive = true;
        Objective = UI.endTurn;
        StartCoroutine(highlight());
    }
    private void Stage7()
    {
        UI.Tutorial.SetActive(true);
        if (!coroutineplaying) linenumber = 0;

        if (isVoiceLinePlaying == true) return;

        isVoiceLinePlaying = true;
        coroutineplaying = true;

        if (linenumber <= lineNamesEnd.Count - 1)
        {
            Debug.Log(linenumber);

            StartCoroutine(PlayVoiceLine(lineNamesEnd[linenumber]));
            
            linenumber++;

        }
        else
        {
            UI.Tutorial.SetActive(false);
            levelManager.win.SetActive(true);
        }

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
    

}
