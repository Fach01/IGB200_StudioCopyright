using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    public List<GameObject> hand;
    public GameObject cardPrefab;

    float cardWidth = 80f;

    private void Start()
    {
        // Initialize the hand
        hand = new List<GameObject>();
    }

    private void Update()
    {
        OrderCards();
    }

    public void AddCard(GameObject card)
    {
        /* TODO: Animate placing the card in the hand
         * get new card position in hand and move it there
         */

        // Add the card to the player's hand
        card.transform.SetParent(transform, false);
        hand.Add(card);
    }

    public void RemoveCard(GameObject card)
    {
        // Remove the card from the player's hand
        // hand.Remove(card);
    }

    public void OrderCards()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            // Update the card's position
            float xPosition = (i - (hand.Count - 1) / 2f) * cardWidth;

            // TODO: Animate moving the card
            Vector3 localPosition = hand[i].transform.localPosition;
            localPosition.x = xPosition;
            hand[i].transform.localPosition = localPosition;
        }
    }
}
