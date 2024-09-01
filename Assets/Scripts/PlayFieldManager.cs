/* TODO: this file is a temp file */
using System.Collections.Generic;
using UnityEngine;

public class PlayFieldManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> cards = new List<GameObject>();
    private int capacity = 3;

    private void Start()
    {

    }

    private float distance = 24f;
    private void Update()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject card = cards[i];

            float cardPositionX = (cards.Count - 1) * (-distance / 2f) + (i * distance);
            // Debug.Log(cardPositionX);
            Vector3 cardPosition = new Vector3(cardPositionX, 0f, 0f) + transform.position;
            card.transform.position = cardPosition;
        }
    }

    public bool AddCard(GameObject card)
    {
        if (cards.Contains(card) || cards.Count >= capacity)
        {
            Debug.Log("Cannot play this card!!");
            return false;
        }
        cards.Add(card);
        card.transform.SetParent(transform, false);
        return true;
    }
}
