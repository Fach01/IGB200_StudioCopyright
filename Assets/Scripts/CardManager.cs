using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private TMP_Text cost;
    private TMP_Text name;
    private TMP_Text description;
    public Card card;

    private void Awake()
    {
        cost = transform.Find("Cost").GetComponent<TMP_Text>();
        name = transform.Find("Name").GetComponent<TMP_Text>();
        description = transform.Find("Description").GetComponent<TMP_Text>();
    }

    public void SetCost(string cost)
    {
        this.cost.text = cost;
    }
    public void SetName(string name)
    {
        this.name.text = name;
    }
    public void SetDescription(string description)
    {
        this.description.text = description;
    }

    public void SetCard(Card card)
    {
        this.card = card;
        cost.text = card.cost.ToString();
        name.text = card.name;
        if (card.ability != null)
        {
            description.text = card.ability.description;
            card.description = card.ability.description;
        }
        else
        {
            description.text = card.description;
        }
    }
}
