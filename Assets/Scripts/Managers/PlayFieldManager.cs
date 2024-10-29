using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayFieldManager : MonoBehaviour
{
    public GameObject levelManager;

    public GameObject player;

    public List<GameObject> cards = new();

    private PlayerManager playerController;
    private int capacity = 5;

    public GameObject playAbility;

    private void Start()
    {
        playAbility.SetActive(false);

        playerController = player.GetComponent<PlayerManager>();

        for (int i = 0; i < capacity; i++)
        {
            cards.Add(null);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {

            }
        }
    }

    public bool AddCard(GameObject currentCard)
    {
        if (cards.Contains(currentCard))
        {
            return false;
        }

        bool added = false;
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] == null)
            {
                cards[i] = currentCard;
                currentCard.transform.SetParent(transform, false);
                playerController.hand.GetComponent<HandManager>().hand.Remove(currentCard);

                added = true;
                break;
            }
        }
        return added;
    }

    private GameObject curCard;
    public GameObject getCurCard() => curCard;
    public bool PlayCurrentCard(GameObject currentCard)
    {

        if (AddCard(currentCard)) {
            Card cardDetails = currentCard.GetComponent<CardManager>().m_card;

            if (cardDetails.type != CardType.Planner && cardDetails.ability != null)
            {
                playerController.endTurn.GetComponent<Button>().interactable = false;
                currentCard.transform.localPosition = new Vector3(-260f, 20f, 0);
                currentCard.GetComponent<CardManager>().locked = true;
                playAbility.SetActive(true);
                playAbility.GetComponent<AbilityUI>().SetCard(currentCard);

                curCard = currentCard;
            }
            else
            {
                OrderCards();
                if (currentCard != null)
                {
                    currentCard.GetComponent<CardManager>().ScaleCard();
                    currentCard.GetComponent<CardManager>().Unlock();
                    currentCard.transform.Translate(0, 20f, 0);
                }
            }
            return true;
        }

        return false; 
    }

    public bool MoveCardToHand(GameObject card) {
        if (cards.Contains(card))
        {
            cards.Remove(card);
            cards.Add(null);
            playerController.hand.GetComponent<HandManager>().AddCard(card);
            card.GetComponent<CardManager>().ScaleCard();
            return true;
        }
        return false;
    }

    public void DiscardAll()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] != null)
            {
                Destroy(cards[i]);
                cards[i] = null;
            }
        }
    }

    public void OrderCards()
    {
        int steps = 360 / capacity;
        int offset = 90;
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] == null)
            {
                continue;
            }
            float xPosition = Mathf.Cos(Mathf.Deg2Rad * (steps * i + offset)) * 100;
            float yPosition = Mathf.Sin(Mathf.Deg2Rad * (steps * i + offset)) * 90 - 8f;
            cards[i].transform.localPosition = new Vector3(xPosition, yPosition, 0f);
        }
    }

    public void ToggleActivatePlayfield(bool activate)
    {
        foreach (GameObject card in cards)
        {
            if (card != null)
            {
                card.GetComponent<CardManager>().Unlock();
                Button cardButton = card.GetComponent<Button>();
                if (cardButton != null)
                {
                    cardButton.interactable = activate;
                }
            }
        }
    }

    public bool IsEmpty()
    {
        foreach(GameObject card in cards)
        {
            if (card != null)
            {
                return false;
            }
        }
        return true;
    }

    public int Size()
    {
        int size = 0;
        foreach (GameObject card in cards)
        {
            if (card != null)
            {
                size += 1;
            }
        }
        return size;
    }
}
