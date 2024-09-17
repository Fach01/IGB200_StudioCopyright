using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    public GameObject playField;
    public TMP_Text text;

    public void OnPlayAbility()
    {
        // activate ability
        Debug.Log("ability!");
        playField.GetComponent<PlayFieldManager>().OrderCards();
        this.gameObject.SetActive(false);
    }

    public void OnPlayCard()
    {
        playField.GetComponent<PlayFieldManager>().OrderCards();
        this.gameObject.SetActive(false);

    }

    public void SetText(Card card)
    {
        text.text = $"Play {card.abilityName} for {card.abilityCost}?";
    }
    
}
