using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System;

public class Tutorialentryscene : Tutorial
{
    public TMP_Text text;
    public List<string> lineNames;
    private int linenumber = 0;
    public GameObject Fade;
    private bool entranceplayed = false;


    private void Awake()
    {
        Invoke(nameof(Play), 1f);
    }
    private void Play()
    {
       StartCoroutine(PlayVoiceLine(lineNames[linenumber]));
    }


    // Update is called once per frame
    new void Update()
    {
        if (!entranceplayed)
        {
            GameManager.instance.Foreground = Fade;
            StartCoroutine(GameManager.instance.TransitionIn());
            entranceplayed=true;
        }
        if (isVoiceLinePlaying && Input.GetKeyDown(KeyCode.Space)) Skip();
    }
    public new void Skip()
    {
        AudioManager.instance.StopDialouge();

        EndText();
    }

    public new IEnumerator PlayVoiceLine(string name)
    {
        Tutoriallines T = Array.Find(lines, x => x.LineName == name);
        string newtext = T.Line;
        string voiceLine = T.AudioClipName;

        linenumber++;
        isVoiceLinePlaying = true;
        AudioManager.instance.PlayDialouge(voiceLine);
        text.text = newtext;

        if (voiceLine != "") { yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); }
        else { yield return new WaitForSeconds(3f); }

        EndText();
    }
    public new void EndText()
    {
        base.EndText();
        if(linenumber < lineNames.Count)
        {

            StartCoroutine(PlayVoiceLine(lineNames[linenumber]));
        }
        else 
        {

            GameManager.instance.ChangeScene("Tutorial 1"); 
        }
        
    }
}
