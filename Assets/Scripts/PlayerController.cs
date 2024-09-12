using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject levelManager;
    public GameObject hand;
    public GameObject playField;

    public GameObject selectedCard;

    public Phase phase;

    private int actionPoints = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawCard(GameObject card)
    {
        // TODO: Check if the player has enough action points to draw a card

        // Add the card to the player's hand
        hand.GetComponent<HandController>().AddCard(card);
    }

    public void SelectCard(GameObject card)
    {
        if (selectedCard != null)
        {
            selectedCard.transform.Translate(0, -20f, 0);
        }

        card.transform.Translate(0, 20f, 0);
        selectedCard = card;
    }

    public void DiscardCard()
    {
        hand.GetComponent<HandController>().RemoveCard(selectedCard);
    }
}
