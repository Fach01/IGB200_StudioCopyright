using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckManager : MonoBehaviour
{
    public GameObject levelManager;

    public GameObject player;

    public GameObject cardPrefab;

    public TMP_Text label;

    public List<Card> deck;

    private PlayerManager PlayerManager;
    private bool outOfCards = false;

    private void Start()
    {
        PlayerManager = player.GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (!outOfCards && deck.Count <= 0)
        {
            outOfCards = true;
            GetComponent<Button>().interactable = false;
            label.text = "Deck is empty!";  
        } 
        else if (outOfCards && deck.Count > 0)
        {
            outOfCards = false;
            GetComponent<Button>().interactable = true;
            label.text = "Deck\n($10,000)";
        }
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
