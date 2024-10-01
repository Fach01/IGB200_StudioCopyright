using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSnapshot : MonoBehaviour
{
    public GameObject deck;

    public List<GameObject> cards = new List<GameObject>();

    public void AddCard()
    {
        GameObject card = deck.GetComponent<DeckManager>().DrawCard();
        cards.Add(card);
        card.transform.SetParent(transform, false);
        OrderCards();
    }

    private void OrderCards()
    {
        float cardWidth = 100f; // Horizontal spacing between cards
        float cardHeight = 110f; // Vertical spacing between rows
        int cardsPerRow = 2; // Number of cards per row
        float heightOffset = 60f;

        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] == null)
            {
                continue;
            }

            // Calculate the row and column index based on the current card index
            int row = i / cardsPerRow;
            int column = i % cardsPerRow;

            // Calculate the x and y positions for each card
            float xPosition = column * cardWidth - (cardWidth / 2f); // Offset to center the cards
            float yPosition = -row * cardHeight + heightOffset;

            // Set the card's local position
            cards[i].transform.localPosition = new Vector3(xPosition, yPosition, 0f);
        }
    }

    public void ReturnCards()
    {
        foreach (GameObject card in cards)
        {
            deck.GetComponent<DeckManager>().AddCardToDeck(card);
            
        }
        cards.Clear();
        this.gameObject.SetActive(false);
    }
  
}
