using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventManager : MonoBehaviour
{
    public bool eventActive;
    public string currentEvent;
    public GameEvent nextEvent;

    public GameObject eventPrompt;
    public TMP_Text promptText;
    public TMP_Text rainyDayText;

    private int turnsRemaining;

    private void Start()
    {
        eventActive = false;
        rainyDayText.text = "";
    }

    public void PlayEvent()
    {
        switch (nextEvent)
        {
            case GameEvent.Flood:
                StartCoroutine(Flood());
                break;
            case GameEvent.SickDay:
                StartCoroutine(SickDay());
                break;
            default:
                GetComponent<LevelManager>().player.GetComponent<PlayerManager>().phase = Phase.End;
                GetComponent<LevelManager>().phaseplaying = false;
                break;
        }
    }
    public IEnumerator Flood()
    {
        Debug.Log(eventActive);
        
        if (!eventActive)
        {
            eventActive = true;
            currentEvent = "Flood";

            
            eventPrompt.SetActive(true);
            promptText.text = "There's heavy rain forecasted the next few days. Be careful...";

            turnsRemaining = 3;
            rainyDayText.text = "3 days of rain ahead.";

            nextEvent = GameEvent.None;
        }


        while (eventActive && turnsRemaining > 0)
        {
            rainyDayText.text = turnsRemaining + " days of rain ahead.";
            turnsRemaining--;
            

            GetComponent<LevelManager>().player.GetComponent<PlayerManager>().phase = Phase.End;
            GetComponent<LevelManager>().phaseplaying = false;

            yield return new WaitForSeconds(10f);

            yield return new WaitUntil(() => GetComponent<LevelManager>().player.GetComponent<PlayerManager>().phase == Phase.Event);
        }

        rainyDayText.text = "";
        eventPrompt.SetActive(true);
        promptText.text = "All that rain caught up! The jobsite is flooding, everyone go home!";
        turnsRemaining = 0;

        //remove all cards
        GetComponent<LevelManager>().playField.GetComponent<PlayFieldManager>().DiscardAll();

        eventActive = false;
        yield break;
    }
    public IEnumerator SickDay()
    {
        // Debug.Log("hi");
        eventActive = true;
        currentEvent = "Sick Day";

        eventPrompt.SetActive(true);
        promptText.text = "Oh No! Someone has gotten ill! They won't be able to work their next shift.";

        int cards = 0;
        for (int i = 0; i < GetComponent<LevelManager>().playField.GetComponent<PlayFieldManager>().cards.Count; i++)
        {
            if (GetComponent<LevelManager>().playField.GetComponent<PlayFieldManager>().cards[i] != null)
            {
                cards++;
            }
        }
        int cardIndex = Random.Range(0, cards);
        GameObject card = GetComponent<LevelManager>().playField.GetComponent<PlayFieldManager>().cards[cardIndex];
        card.GetComponent<CardManager>().sick = true;

        GetComponent<LevelManager>().player.GetComponent<PlayerManager>().phase = Phase.End;
        GetComponent<LevelManager>().phaseplaying = false;

        nextEvent = GameEvent.None;
        yield return null;
    }
}