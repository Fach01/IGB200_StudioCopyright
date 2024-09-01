using System.Collections.Generic;
using UnityEngine;

public class CardContainer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> cards = new List<GameObject>();

    private void Start()
    {

    }

    private void Update()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject card = cards[i];

            Vector3 cardPosition = card.transform.position;
            cardPosition.x = (cards.Count - 1) * -0.5f + i * 12f;

            card.transform.position = cardPosition;
        }
    }

    public void AddCard(GameObject card)
    {
        cards.Add(card);
    }
}
