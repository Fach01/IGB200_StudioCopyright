using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject levelManager;
    public GameObject hand;
    public GameObject playField;
    public GameObject discard;

    public GameObject selectedCard;

    public Phase phase;

    private int actionPoints = 2;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void HandleMouseEvents()
    {
        /* On mouse up check if the player is deselecting a card
         * click on play field with card already in play field
         * click 
         * 
         */
        if (Input.GetMouseButtonUp(0))
        {

        }
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

    public void PlaySelectedCard()
    {
        playField.GetComponent<PlayFieldManager>().PlayCurrentCard();
    }

    public void DrawCard(GameObject card)
    {
        // TODO: Check if the player has enough action points to draw a card

        // Add the card to the player's hand
        hand.GetComponent<HandController>().AddCard(card);
    }

    public void DiscardCard()
    {
        /* TODO: Animate removing the card from the hand
         * and move card to discard pile
         */
        Destroy(selectedCard);
    }
}
