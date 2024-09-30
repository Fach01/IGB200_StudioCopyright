using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    public GameObject levelManager;

    public List<GameObject> hand = new();

    private float cardWidth = 80f;

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

    public bool SearchForCard(GameObject card)
    {
        return hand.Contains(card);
    }

    public void OrderCards()
    {
        if (hand.Count <= 4)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i] == null)
                {
                    // scuffed fix for destroying cards idk a better fix tbh
                    hand.Remove(hand[i]);
                    continue;
                }

                // Update the card's position
                float xPosition = (i - (hand.Count - 1) / 2f) * cardWidth;

                // TODO: Animate moving the card
                Vector3 localPosition = hand[i].transform.localPosition;
                localPosition.x = xPosition;
                hand[i].transform.localPosition = localPosition;
            }
        }
        else
        {
            float startPosition = (-transform.GetComponent<RectTransform>().rect.width + cardWidth) / 2f + 1.5f * cardWidth / hand.Count;
            float step = (transform.GetComponent<RectTransform>().rect.width - cardWidth) / hand.Count;
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i] == null)
                {
                    // scuffed fix for destroying cards idk a better fix tbh
                    hand.Remove(hand[i]);
                    continue;
                }

                // Update the card's position
                float xPosition = startPosition + i * step;

                // TODO: Animate moving the card
                Vector3 localPosition = hand[i].transform.localPosition;
                localPosition.x = xPosition;
                hand[i].transform.localPosition = localPosition;
            }
        }
    }

    public void ToggleActivateHand(bool activate)
    {
        foreach (GameObject card in hand)
        {
            Button cardButton = card.GetComponent<Button>();
            if (cardButton != null)
            {
                cardButton.interactable = activate;
            }
        }
    }
}
