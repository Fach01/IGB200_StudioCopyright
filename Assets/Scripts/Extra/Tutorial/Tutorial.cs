using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public abstract class Tutorial : MonoBehaviour
{
    public UIManager? UI;
    public LevelManager? levelManager;
    public HandManager? HandManager;
    public PlayerManager? playerManager;

    public bool handActive = false;
    protected bool isVoiceLinePlaying = false;
    protected bool coroutineplaying = false;

    public GameObject Goal;
    public GameObject Objective;
    public GameObject guidance;

    public Tutoriallines[] lines;


    public void Update()
    {
        HandManager.ToggleActivateHand(handActive);
        if (isVoiceLinePlaying && Input.GetKeyDown(KeyCode.Space)) Skip();
    }
    public IEnumerator PlayVoiceLine(string name) //nullable untill we get the voice lines
    {
        Tutoriallines T = Array.Find(lines, x => x.LineName == name);
        string text = T.Line;
        string voiceLine = T.AudioClipName;

        isVoiceLinePlaying = true;
        handActive = false;

        AudioManager.instance.PlaySFX(voiceLine);
        UI.TutorialActive(text);

        if (voiceLine != null) { yield return new WaitUntil(() => AudioManager.instance.sfxSource.isPlaying == false); }
        else { yield return new WaitForSeconds(3f); }
        EndText();

    }
    public IEnumerator highlight()
    {
        coroutineplaying = true;
        while (Goal != Objective)
        {

            GameObject highlight = Instantiate(guidance, Objective.transform.position, Objective.transform.rotation, Objective.transform.parent);
            yield return new WaitForSeconds(0.6f);
            Destroy(highlight);

        }
    }
    public void SetGoal(GameObject newgoal)
    {
        Goal = newgoal;

    }
    public void EndText()
    {
        isVoiceLinePlaying = false;
        handActive = true;

    }
    public void FinishDialouge()
    {
        UI.Tutorial.gameObject.SetActive(false);
    }
    public void Skip()
    {
        AudioManager.instance.StopSFX();
        EndText();
    }
}
