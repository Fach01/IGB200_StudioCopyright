using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public bool eventActive;
    public string currentEvent;
    public GameEvent nextEvent;

    public GameObject floodPrompt;
    public GameObject sickDayPrompt;

    private void Start()
    {
        eventActive = false;
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
        eventActive = true;
        currentEvent = "Flood";

        int turnsRemaining = 3;

        while ( turnsRemaining > 0)
        {

        }
   
        // set a 3 turn countdown

        GetComponent<LevelManager>().player.GetComponent<PlayerManager>().phase = Phase.End;
        GetComponent<LevelManager>().phaseplaying = false;

        yield return null;
    }
    public IEnumerator SickDay()
    {
        // Debug.Log("hi");
        eventActive = true;
        currentEvent = "Sick Day";

        sickDayPrompt.SetActive(true);

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