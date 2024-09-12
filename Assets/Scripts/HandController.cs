using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    public List<GameObject> hand = new();

    float cardWidth = 80f;

    private void Start()
    {

    }

    private void Update()
    {
        OrderCards();
    }

    public void AddCard(GameObject card)
    {
        /* TODO: Animate placing the card in the hand
         * get new card position in hand and move it there
         * move cards before new card gets added to hand
         */

        // Add the card to the player's hand
        card.transform.SetParent(transform, false);
        hand.Add(card);
    }

    public void OrderCards()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            if (hand[i] == null)
            {
                // scuffed fix for destroying cards idk a better fix tbh
                hand.Remove(hand[i]);
            }

            // Update the card's position
            float xPosition = (i - (hand.Count - 1) / 2f) * cardWidth;

            // TODO: Animate moving the card
            Vector3 localPosition = hand[i].transform.localPosition;
            localPosition.x = xPosition;
            hand[i].transform.localPosition = localPosition;
        }
    }
}
