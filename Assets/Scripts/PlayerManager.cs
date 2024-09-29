using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject levelManager;
    public GameObject uiManager;
    public GameObject playField;
    public GameObject hand;
    public GameObject discard;
    public GameObject deck;
    public GameObject endTurn;

    public GameObject selectedCard;

    public Phase phase;

    private UIManager UIManager;

    private int actionPoints = 2;
    private bool cardSelected = false;

    private GameObject clickedButton;

    private void Awake()
    {
        Setup();
    }

    // Start is called before the first frame update
    private void Start()
    {
        UIManager = uiManager.GetComponent<UIManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        UIManager.SetActionPointsText(actionPoints.ToString());
        UIManager.SetPhaseText(phase);
    }

    private void LateUpdate()
    {
        // called after ui buttons are clicked
        HandleMouseEvents();
    }

    private void HandleMouseEvents()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (clickedButton == null && !cardSelected)
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
            if (playField.GetComponent<PlayFieldManager>().PlayCurrentCard(selectedCard))
            {
                actionPoints -= 1;
            }
            SelectCard(null);
        }
    }

    public void DecreaseActionPoints()
    {

        if (actionPoints > 0 && phase != Phase.Play && phase != Phase.Event && phase != Phase.End)
        {
            actionPoints -= 1;
            
        }
    }

    public void DrawCard(GameObject card)
    {
        hand.GetComponent<HandManager>().AddCard(card);
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
