using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject deckObject;
    private Deck deck;

    private List<GameObject> hand = new List<GameObject> { };

    private int rows()
    {
        if (hand.Count <= 0)
        {
            return 1;
        }
        // rows of 4
        int rows = Mathf.CeilToInt(hand.Count / 4);
        if (rows > 0)
        {
            return rows;
        }
        return 1;
        
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
        Vector3 position;
        if (hand.Count > 0)
        {
            int row = Mathf.CeilToInt(posInHand / 4f);
            Debug.Log("pos in hand = " + posInHand + " num rows = " + numRows);
            //fix this line:
            position = new Vector3(width / posInHand - midx, height / numRows * row - midy, 0f);
        }
        else
        {
            position = new Vector3(width / 16 - midx, height / numRows - midy, 0f);
        }

        GameObject newCard = Instantiate(cardPrefab, position, transform.rotation, transform);
        CardManager cardManager = newCard.GetComponent<CardManager>();
        cardManager.SetCost(card.cost.ToString());
        cardManager.SetName(card.name);
        cardManager.SetDescription(card.description);

        hand.Add(newCard);
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
