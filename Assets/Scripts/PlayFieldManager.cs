using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

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
    }

    public bool PlayCurrentCard()
    {
        GameObject currentCard = playerController.selectedCard;

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

        Card cardDetails = currentCard.GetComponent<CardManager>().m_card;

        if (cardDetails.type != CardType.Planner)
        {
            currentCard.transform.position = new Vector3(80f, 220f, 0);
            playAbility.SetActive(true);
            playAbility.GetComponent<AbilityUI>().SetCard(cardDetails);
        }
        else
        {
            OrderCards();
            if (currentCard != null)
            {
                currentCard.transform.Translate(0, 20f, 0);
            }
        }

        return added; 
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
}
