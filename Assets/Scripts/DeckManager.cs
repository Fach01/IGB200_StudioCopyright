using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public GameObject levelManager;

    public GameObject player;

    public GameObject cardPrefab;

    public List<Card> deck;

    private PlayerManager PlayerManager;

    private void Start()
    {
        PlayerManager = player.GetComponent<PlayerManager>();
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
        PlayerManager.DrawCard(newCard);
    }
}
