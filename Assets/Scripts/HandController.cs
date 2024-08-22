using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject deckObject;
    private Deck deck;

    private List<Card> hand;

    private int rows()
    {
        if (hand.Count <= 0) {
            return 1;
        }
        // rows of 4
        return Mathf.CeilToInt(hand.Count / 4);
    }



    // Start is called before the first frame update
    void Start()
    {
        deck = deckObject.GetComponent<Deck>();

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateNewCard(Card card)
    {
        int numRows = rows();
        int posInHand = hand.Count + 1;

        int width = 12 * 4;
        int height = 15 * numRows;

        float midx = (12f * 3f) / 2f;
        float midy = (15f * (numRows - 1)) / 2f;
        // x width: 10, y width: 13
        int row = Mathf.CeilToInt(posInHand / 4f);
        int cardsInRow = row == numRows ? hand.Count % 4 : 4;
        Vector3 position = new Vector3(width / cardsInRow * (posInHand % 4) - midx, height / numRows * row - midy, 0f);

        GameObject newCard = Instantiate(cardPrefab, position, transform.rotation, transform);
        CardManager cardManager = newCard.GetComponent<CardManager>();
        cardManager.SetCost(card.cost.ToString());
        cardManager.SetName(card.name);
        cardManager.SetDescription(card.description);
    }

    public void DrawCard()
    {
        if (deck && deck.cardDeck.Count > 0)
        {
            Card card = deck.DrawRandomCard();
            if (card != null)
            {
                InstantiateNewCard(card);
            }
        }
    }
}
