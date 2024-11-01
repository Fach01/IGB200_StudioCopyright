using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class PlayFieldManager : MonoBehaviour
{
    public GameObject levelManager;

    public GameObject player;

    public List<GameObject> cards = new();

    private PlayerManager playerController;
    private int capacity = 5;

    public GameObject playAbility;
    public GameObject pause;

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
        /*if (curCard == null) return;

        int cardIndex = cards.IndexOf(curCard);

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool raycast = Physics.Raycast(ray, out hit, Mathf.Infinity);

            // Vector3 end = Camera.main.transform.position + ray.direction * 1000f;

            if (!raycast || hit.transform != curCard.transform)
            {
                if (cardIndex != -1)
                {
                    cards[cardIndex] = null;
                    curCard.transform.SetParent(playerController.hand.transform, false);
                    curCard.GetComponent<CardManager>().ScaleCard();

                    playerController.endTurn.GetComponent<Button>().interactable = true;
                    curCard.transform.localPosition = new Vector3(0f, 0f, 0f);
                    foreach (GameObject card in
                             player.GetComponent<PlayerManager>().hand.GetComponent<HandManager>().hand)
                    {
                        card.GetComponent<CardManager>().Unlock();
                    }

                    curCard.GetComponent<CardManager>().locked = false;
                    playAbility.SetActive(false);
                    // playAbility.GetComponent<AbilityUI>().SetCard(null);

                    player.GetComponent<PlayerManager>().hand.GetComponent<HandManager>().AddCard(curCard);
                    player.GetComponent<PlayerManager>().hand.GetComponent<HandManager>().OrderCards();

                    curCard = null;

                    player.GetComponent<PlayerManager>().AddActionPoint();
                }
            }
        }*/
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
                currentCard.transform.localPosition = new Vector3(-260f, 40f, 0);
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
