using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject deckObject;
    private Deck deck;

    private int rowSize = 5;

    private List<GameObject> hand = new List<GameObject> { };

    private int rows()
    {
        if (hand.Count <= 0)
        {
            return 1;
        }
        // rows of 5
        int rows = Mathf.CeilToInt(hand.Count / rowSize);
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

        int width = 12 * rowSize;
        int rowHeight = 15;
        float midx = (12f * 3f) / 2f;
        // x width: 10, y width: 13

        Vector3 position;

        if (hand.Count > 0)
        {
            int posInHand = hand.Count + 1;

            int rowPos = posInHand % rowSize;

            // check for new row 
            if (rowPos == 1)
            {
                //if there is a new row, move everything up by just over height of a card
                foreach (GameObject obj in hand)
                {
                    Vector3 currentPos = obj.transform.localPosition;
                    currentPos.y = obj.transform.localPosition.y + rowHeight;
                    obj.transform.localPosition = currentPos;
                }
            }
            //fix this line:
            position = new Vector3(width / rowSize * rowPos - midx, 0f, 15f);
        }
        else
        {
            position = new Vector3(width / rowSize - midx, 0f, 15f);
        }

        GameObject newCard = Instantiate(cardPrefab, position, transform.rotation, transform);
        newCard.transform.localPosition = position;
        CardManager cardManager = newCard.GetComponent<CardManager>();
        cardManager.SetCard(card);

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
            else
            {
                Debug.Log("out of cards!");
                //TODO: add feedback for this
            }
        }
    }

    public void DeleteCard(GameObject card)
    {  
        // remove card from hand
        for (int i = 0; i < hand.Count;  i++)
        {
            if (hand[i] == card)
            {
                hand.RemoveAt(i);
            }
        }

        Destroy(card);

        // reorder cards
        int width = 12 * rowSize;
        int rowHeight = 15;
        float midx = (12f * 3f) / 2f;

        for (int i = 0; i < hand.Count; i++)
        {

            int row = Mathf.CeilToInt(i / rowSize);
            int rowPos = i % rowSize;

            Vector3 position = new Vector3(width / rowSize * rowPos - midx, rowHeight * row, 15f);
            hand[i].transform.localPosition = position;
        }

    }
}
