using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private TMP_Text cost;
    private TMP_Text name;
    private TMP_Text description;

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
}
