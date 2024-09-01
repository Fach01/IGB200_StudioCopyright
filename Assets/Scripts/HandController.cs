using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject deckObject;
    private Deck deck;
    public TMP_Text deckText;
    public Button drawButton;

    private int rowSize = 5;

    public List<GameObject> hand = new List<GameObject> { };

    private int rows()
    {
        if (hand.Count <= 0)
        {
            return 1;
        }
        int rows = Mathf.CeilToInt(hand.Count / (float)rowSize);
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

    private float CalculatePosX(int posInHand)
    {
        //= row == rows ? cardAssets.Length % 4 : 4;
        int posInRow = posInHand % rowSize;
        int cardSize = 12; // width of each card with padding
        float initPosX = 0; // the local position of the first card

        if (posInRow == 0)
        {
            return initPosX;
        }
        return initPosX + (cardSize * posInRow);

    }

    private void InstantiateNewCard(Card card)
    {

        int rowHeight = 15;

        // card size is x width: 10, y width: 13

        Vector3 position;

        if (hand.Count > 0)
        {
            int posInHand = hand.Count;

            // check for new row 
            if (hand.Count != 1 && posInHand % rowSize == 0)
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
            position = new Vector3(CalculatePosX(posInHand), 0f, 15f);
        }
        else
        {
            position = new Vector3(CalculatePosX(0), 0f, 15f);
        }

        GameObject newCard = Instantiate(cardPrefab, new Vector3 (0,0,0), transform.rotation, transform);
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
        }
        else
        {
            deckText.text = "Out of cards!";
            drawButton.interactable = false;
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

        ReorderCards(hand);
        

    }

    public void ReorderCards(List<GameObject> cards)
    {
        int rowHeight = 15;

        for (int i = 0; i < cards.Count; i++)
        {

            int row = Mathf.CeilToInt(i / rowSize);
            int rowPos = i % rowSize;

            Vector3 position = new Vector3(CalculatePosX(i), rowHeight * row, 15f);
            hand[i].transform.localPosition = position;
        }
    }
}
