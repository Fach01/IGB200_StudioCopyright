using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorailLevel2 : Tutorial
{
    private bool drawtutplayed = false;

    private bool line1played = false;
    private bool line2played = false;

    private new void Update()
    {
        base.Update();

        if (playerManager.selectedCard != null)
        {
            string ability = playerManager.selectedCard.GetComponent<CardManager>().m_card.abilityName;
            if (ability == "Farsight" && !line1played) { Farsight(); line1played = true; }
            else if (ability == "Foundation" && !line2played) { Foundation(); line2played = true; }
        }
        if (playerManager.phase == Phase.Setup && levelManager.turn != 1)
        {
            Draw();
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
        StartCoroutine(PlayVoiceLine("Start"));
    }
    public void Farsight()
    {
        StartCoroutine(PlayVoiceLine("Farsight"));
    }
    public void Foundation()
    {
        StartCoroutine(PlayVoiceLine("Foundation"));
    }
    public void Draw()
    {
        if (!drawtutplayed)
        {
            PlayVoiceLine("Draw");
            drawtutplayed = true;
        }

    }
    public new IEnumerator PlayVoiceLine(string name)
    {
        UI.Tutorial.SetActive(true);

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
    public new void EndText()
    {
        base.EndText();
        UI.Tutorial.SetActive(false);
    }
    

}
