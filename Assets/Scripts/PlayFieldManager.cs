using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class PlayFieldManager : MonoBehaviour
{
    public GameObject player;

    public List<GameObject> cards = new();

    private PlayerController playerController;
    private int capacity = 5;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();

        for (int i = 0; i < capacity; i++)
        {
            cards.Add(null);
        }
    }

    private void Update()
    {
    }

    public void PlayCurrentCard()
    {
        GameObject currentCard = playerController.selectedCard;
        currentCard.transform.SetParent(transform, false);

        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i] == null)
            {
                cards[i] = currentCard;
                playerController.hand.GetComponent<HandController>().hand.Remove(currentCard);
                break;
            }
        }
        OrderCards();
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
