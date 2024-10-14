using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    public GameObject levelManager;
    public GameObject uiManager;
    public GameObject playField;
    public GameObject hand;
    public GameObject discard;
    public GameObject deck;
    public GameObject endTurn;

    public KeyCode pause = KeyCode.Escape;
    private bool pauseonoff = false;
    public GameObject selectedCard;

    public Phase phase;

    private UIManager UIManager;
    private LevelManager LevelManager;

    private int actionPoints = 2;
    private bool cardSelected = false;

    public GameObject clickedButton {get;private set;}


    private void Awake()
    {
        Setup();
    }

    // Start is called before the first frame update
    private void Start()
    {
        UIManager = uiManager.GetComponent<UIManager>();
        LevelManager = levelManager.GetComponent<LevelManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        UIManager.SetActionPointsText(actionPoints.ToString());
        UIManager.SetPhaseText(phase);
        if (Input.GetKeyDown(pause)) { pauseonoff = !pauseonoff;  UIManager.Pause(LevelManager.highscore, pauseonoff); }
    }

    private void LateUpdate()
    {
        // called after ui buttons are clicked
        HandleMouseEvents();
    }
    private void HandleMouseEvents()
    {
        if (Input.GetMouseButtonDown(1) && selectedCard != null)
        {
            UIManager.HighlightCard(selectedCard);

        }
        if (Input.GetMouseButtonUp(0))
        {
            if (clickedButton != null && !cardSelected)
            {
                SelectCard(null);
            }
        }

        cardSelected = false;
        clickedButton = null;
    }

    public void ClickedGameObject(GameObject clickedObject)
    {
        clickedButton = clickedObject;
    }
    public void SelectCard(GameObject card)
    {
        if (selectedCard != null)
        {
            selectedCard.transform.Translate(0, -20f, 0);
        }
        if (card != null)
        {
            card.transform.Translate(0, 20f, 0f);
        }
        selectedCard = card;
        cardSelected = true;
    }

    public void PlaySelectedCard()
    {
        if (selectedCard == null)
        {
            return;
        }
        if (actionPoints > 0 && phase == Phase.Setup)
        {
            if (levelManager.GetComponent<LevelManager>().tutorial != null && levelManager.GetComponent<LevelManager>().level == level.level1)
            {
                levelManager.GetComponent<LevelManager>().tutorial.GetComponent<Tutorial>().Goal = playField;
            }
            if (playField.GetComponent<PlayFieldManager>().PlayCurrentCard(selectedCard))
            {
                actionPoints -= 1;
            }
            SelectCard(null);
        }
    }

    public bool DecreaseActionPoints()
    {

        if (actionPoints > 0 && phase != Phase.Play && phase != Phase.Event && phase != Phase.End)
        {
            actionPoints -= 1;
            return true;
            
        }
        return false;
    }

    public void DrawCard()
    {
        AudioManager.instance.PlaySFX("Pickup Card");
        GameObject card = deck.GetComponent<DeckManager>().DrawCard();
        if (card != null) hand.GetComponent<HandManager>().AddCard(card);
        else Debug.Log("DeckEmpty");
    }

    public IEnumerator DrawXCards(int x)
    {
        for (int i = 0; i < x; i++)
        {
            DrawCard();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void DiscardCard()
    {
        /* TODO: Animate removing the card from the hand
         * and move card to discard pile
         */
        if (selectedCard == null)
        {
            return;
        }
        if (selectedCard.transform.parent == playField.transform)
        {
            Destroy(selectedCard);
        }
    }

    public void Setup()
    {
        actionPoints = 4;
    }

    public void ResetPlayer()
    {
        actionPoints = 2;
    }
}
