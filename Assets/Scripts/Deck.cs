using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> cardDeck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Card DrawRandomCard()
    {
        if (cardDeck.Count > 0)
        {
            int cardNum = Random.Range(0, (cardDeck.Count - 1));
            Card card = cardDeck[cardNum];
            cardDeck.RemoveAt(cardNum);

            return card;
        }
        return null;
    }
}
