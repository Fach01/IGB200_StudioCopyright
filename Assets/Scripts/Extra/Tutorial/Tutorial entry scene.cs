using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System;

public class Tutorialentryscene : Tutorial
{
    public TMP_Text text;
    private List<string> lineNames;
    private int linenumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayVoiceLine(lineNames[linenumber]));
    }

    // Update is called once per frame
    new void Update()
    {
        if (isVoiceLinePlaying && Input.GetKeyDown(KeyCode.Space)) Skip();
    }

    public new IEnumerator PlayVoiceLine(string name)
    {
        Tutoriallines T = Array.Find(lines, x => x.LineName == name);
        string newtext = T.Line;
        string voiceLine = T.AudioClipName;

        linenumber++;
        isVoiceLinePlaying = true;
        AudioManager.instance.PlaySFX(voiceLine);
        text.text = newtext;

        if (voiceLine != null) { yield return new WaitUntil(() => AudioManager.instance.sfxSource.isPlaying == false); }
        else { yield return new WaitForSeconds(3f); }

        EndText();
    }
    public new void EndText()
    {
        isVoiceLinePlaying = false;
        if(linenumber < lineNames.Count) { StartCoroutine(PlayVoiceLine(lineNames[linenumber]));}
        else { GameManager.instance.ChangeScene("Tutorial 1"); }
        
    }
}
