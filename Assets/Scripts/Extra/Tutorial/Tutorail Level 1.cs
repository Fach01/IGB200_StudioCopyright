using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TutorailLevel1 : Tutorial
{
    public List<string> lineNamesStartText;
    public List<string> lineNamesJack;
    public List<string> lineNamesAlex;

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
            tutorialsequence = 4;
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
        if(!isVoiceLinePlaying && linenumber < lineNamesJack.Count) { StartCoroutine(PlayVoiceLine(lineNamesJack[linenumber])); }
        else if (!isVoiceLinePlaying) { UI.Tutorial.SetActive(false); }
        if (Objective == Goal && coroutineplaying)
        {
            StopAllCoroutines();
            Goal = null;
            coroutineplaying = false;
            tutorialsequence = 5;
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
        if (!isVoiceLinePlaying && linenumber < lineNamesAlex.Count) { StartCoroutine(PlayVoiceLine(lineNamesAlex[linenumber])); }
        else if (!isVoiceLinePlaying) { UI.Tutorial.SetActive(false); }
        if (Objective == Goal && coroutineplaying)
        {
            StopAllCoroutines();
            Goal = null;
            coroutineplaying = false;
            tutorialsequence = 5;
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
        foreach (GameObject card in HandManager.hand)
        {
            if ((card.GetComponent<CardManager>().m_card.name) == "Plumber") Objective = card;
        }
        StartCoroutine(highlight());
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

        else { yield return new WaitForSeconds(3f); }

        EndText();

        yield break;

    }
    

}
