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
    public GameObject DrawCard()
    {
        
        if (deck.Count <= 0)
        {
            return null;
        }

        // create a new card from a random card in the deck and set the card's values
        Card randomCard = deck[Random.Range(0, deck.Count)];
        GameObject newCard = Instantiate(cardPrefab);

        newCard.GetComponent<CardManager>().SetCard(randomCard);

        deck.Remove(randomCard);

        Debug.Log("Deck says:" + newCard);

        return newCard;

        // TODO: Animate picking up the card
 
    }

    // return an instantiated card to the deck
    public void AddCardToDeck(GameObject cardObject)
    {
        Card card = cardObject.GetComponent<CardManager>().m_card;
        deck.Add(card);
        Destroy(cardObject);
    }

    public void OnClickDraw()
    {
        // check action points
        if (PlayerManager.DecreaseActionPoints())
        {
            PlayerManager.DrawCard();
            levelManager.GetComponent<LevelManager>().Spend(10000);
        }
        // TODO: handle alternate
    }
}
