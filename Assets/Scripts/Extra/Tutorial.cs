using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public abstract class Tutorial : MonoBehaviour
{
    public UIManager UI;
    public LevelManager levelManager;
    public HandManager HandManager;
    public bool handActive = false;
    protected bool isVoiceLinePlaying = false;
    public void Update()
    {
        HandManager.ToggleActivateHand(handActive);
        if (isVoiceLinePlaying && Input.GetKeyDown("Space")) Skip();
    }
    public IEnumerator PlayVoiceLine( string text, string? voiceLine = null) //nullable untill we get the voice lines
    {
        isVoiceLinePlaying=true;
        UI.Tutorial.gameObject.SetActive(true);
        handActive = false;
        AudioManager.instance.PlaySFX(voiceLine);
        UI.TutorialActive(text);
        yield return new WaitUntil(() => AudioManager.instance.sfxSource.isPlaying == false);
        EndText();

    }
    public void EndText()
    {
        isVoiceLinePlaying = false;
        handActive = true;
        UI.Tutorial.gameObject.SetActive(false);

    }
    public void Skip()
    {
        AudioManager.instance.StopSFX();
        EndText();
    }
}
