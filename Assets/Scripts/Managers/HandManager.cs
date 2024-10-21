using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    public GameObject levelManager;
    public AbilityManager abilityManager;

    public List<GameObject> hand = new();

    private float cardWidth = 80f;
    public bool inAnimation = false;

    private void Update()
    {
        OrderCards();
    }

    public void AddCard(GameObject card)
    {
        // Add the card to the player's hand
        
        AudioManager.instance.PlaySFX("Pickup Card");
        card.GetComponent<CardManager>().cardanimator.SetBool("Initiate", true); // Plays the Intro animation for the cards
        card.transform.SetParent(transform, false);
        hand.Add(card);
        card.GetComponent<CardManager>().Unlock();
        
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
                card.GetComponent<CardManager>().locked = !activate;
                cardButton.interactable = activate;
            }
        }
    }
}
