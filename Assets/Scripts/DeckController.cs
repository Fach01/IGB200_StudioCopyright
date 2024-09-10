using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public GameObject player;
    public GameObject cardPrefab;

    private PlayerController PlayerController;

    public List<Card> deck;

    private void Start()
    {
        PlayerController = player.GetComponent<PlayerController>();
    }

    // picks a random card from the deck and moves it to the player's hand
    public void DrawCard()
    {
        if (deck.Count <= 0)
        {
            return;
        }

        // create a new card from a random card in the deck and set the card's values
        Card randomCard = deck[Random.Range(0, deck.Count)];
        GameObject newCard = Instantiate(cardPrefab);

        newCard.GetComponent<CardManager>().SetCard(randomCard);

        deck.Remove(randomCard);

        // TODO: Animate picking up the card

        // send to playercontroller to handle action points
        PlayerController.DrawCard(newCard);
        
    }
}
