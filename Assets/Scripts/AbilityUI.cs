using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    public GameObject playField;
    public GameObject player;
    public TMP_Text text;

    private Card card = null;

    public void OnPlayAbility()
    {
        if (card != null && card.ability != null)
        {
            card.ability.ActivateAbility(player.GetComponent<PlayerManager>());
        }
        else
        {
            Debug.Log("oh no");
        }
        playField.GetComponent<PlayFieldManager>().OrderCards();
        this.gameObject.SetActive(false);
    }

    public void OnPlayCard()
    {
        playField.GetComponent<PlayFieldManager>().OrderCards();
        this.gameObject.SetActive(false);

    }

    public void SetCard(Card currentCard)
    {
        card = currentCard;
        text.text = $"Play {card.abilityName} for {card.abilityCost}?";
    }
    
}
