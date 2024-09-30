using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    public GameObject playField;
    public GameObject player;
    public TMP_Text text;

    public Button playAbilityButton;
    public Button playCardButton;

    private Card card = null;

    private string defaultText = "Press enter to confirm your choice.";

    public void OnPlayAbility()
    {
        if (card != null && card.ability != null)
        {
            card.ability.ActivateAbility(player.GetComponent<PlayerManager>(), this.gameObject);
            PlayingAbility();
        }
        else
        {
            Debug.Log("oh no");
            playField.GetComponent<PlayFieldManager>().OrderCards();
        }
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

    public void PlayingAbility()
    {
        text.text = card.ability.Description + "\n" + defaultText;

        //disable buttons
        playAbilityButton.gameObject.SetActive(false);
        playCardButton.gameObject.SetActive(false);
        
    }

    public void Reset()
    {
        playAbilityButton.gameObject.SetActive(true);
        playCardButton.gameObject.SetActive(true);

        playField.GetComponent<PlayFieldManager>().OrderCards();
        this.gameObject.SetActive(false);
    }
    
}
