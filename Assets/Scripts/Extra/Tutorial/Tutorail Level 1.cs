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
    public List<string> lineNamesID;
    public List<string> lineNamesJack;
    public List<string> lineNamesAlex;
    public List<string> eventLines;
    public List<string> lineNamesEnd;

    private int linenumber = 0;
    private int tutorialsequence = 1;
    [Header("Highlighted Objects")]
    public GameObject Playfield;
    public GameObject CardOnly;
    public GameObject Draw;

    public bool highlighter = false;
    public bool stopcoroutine = false;

    [Header("Caitlyn Card")]
    public GameObject Caitlyn;
    public GameObject CardType;
    public GameObject ResourceAmount;
    public GameObject BudgetAmount;
    [Header("Jack Card")]
    public GameObject Jack;
    public GameObject Description;

    private new void Update()
    {
        base.Update();
        switch (tutorialsequence)
        {
            case 1:
                ShowCaitlyID(); 
                break;
            case 2:
                Stage2();
                break;
            case 3: 
                HighlightObject(Playfield);
                break;
            case 4:
                ShowJackID();
                break;
            case 5:
                HighlightObject(CardOnly);
                break;
            case 6:
                BudgetID();
                break;
            case 7:
                HighlightObject(Draw);
                break;
            case 8:
                HighlightObject(UI.endTurn);
                break;
            case 9:
                Invoke(nameof(Stage10), 5f);
                break;
            case 10:
                Dialouge(lineNamesEnd);
                break;
            case 11:
                levelManager.win.SetActive(true);
                break;
            default:
                Debug.Log("no stage");
                break;
        }



    }

    void Awake()
    {
        Invoke(nameof(StartTutorial) , 1f) ;
       
    }
    void StartTutorial()
    {
        UI.Tutorial.SetActive(true);
        ShowCaitlyID();
    }
    // initial diaglouge played
    public void ShowCaitlyID()
    {
        Dialouge(lineNamesStartText);

        if (linenumber == 5) 
        {
            Caitlyn.SetActive(true);
            HighlightMidDialouge(ResourceAmount);
        }
        if(linenumber == 6)
        {
            Goal = ResourceAmount;
            HighlightMidDialouge(CardType);
        }
        if(linenumber == 7) { Goal = CardType; Caitlyn.SetActive(false); }
    

    }
    public void ShowJackID()
    {
        Dialouge(lineNamesJack);
        if (linenumber == 2) 
        { 
            Jack.SetActive(true);
            HighlightMidDialouge(Description); 
        }
        if (linenumber == 3)
        { 
            Goal = Description;
            Jack.SetActive(false);
        }
    }
    public void BudgetID()
    {
        Dialouge(lineNamesAlex);
        if (linenumber == 4)
        {
            Caitlyn.SetActive(true);
            HighlightMidDialouge(BudgetAmount);

        }
        else Caitlyn.SetActive(false);
    }
    private bool Dialouge(List<string> lineNames)
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
            tutorialsequence ++;
            isVoiceLinePlaying = false;
            Debug.Log(tutorialsequence);
            AudioManager.instance.StopDialouge();
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
    private void HighlightMidDialouge(GameObject objective)
    {
        if (Goal == objective && highlighter == true)
        {
            highlighter = false;
            return;

        }
        Objective = objective;
        if (highlighter == true) { return; }
        highlighter = true;
        StartCoroutine(highlightMidDialouge());
    }
    private void HighlightObject( GameObject objective)
    {
        if(objective.active == false) { tutorialsequence++; return; }
        if (Objective == Goal && coroutineplaying == true)
        {
            Goal = null;
            coroutineplaying = false;
            tutorialsequence ++;
            StopCoroutine(highlight());
            Debug.Log(tutorialsequence);
            return;
        }
        if (coroutineplaying == true) return;
        coroutineplaying = true;
        handActive = true;
        Objective = objective;

        StartCoroutine(highlight());

    }
    public IEnumerator highlightMidDialouge()
    {
        GameObject highlight = Instantiate(guidance, Objective.transform.position, Objective.transform.rotation, Objective.transform.parent);
        yield return new WaitForSeconds(0.6f);
        Destroy(highlight);
        highlighter = false;
    }
    private void Stage10()
    {
        Dialouge(eventLines);
    }
    public new void Skip()
    {
        base.Skip();
        coroutineplaying=false;
    }
    

}
