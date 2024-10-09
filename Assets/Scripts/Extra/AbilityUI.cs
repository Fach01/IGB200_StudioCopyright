using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    public GameObject playField;
    public GameObject player;
    public GameObject hand;
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
        // toggle cards on
        hand.GetComponent<HandManager>().ToggleActivateHand(true);
        playField.GetComponent<PlayFieldManager>().ToggleActivatePlayfield(true);

        playField.GetComponent<PlayFieldManager>().OrderCards();
        this.gameObject.SetActive(false);

    }

    public void SetCard(Card currentCard)
    {
        // toggle hand and playfield off
        hand.GetComponent<HandManager>().ToggleActivateHand(false);
        playField.GetComponent<PlayFieldManager>().ToggleActivatePlayfield(false);

        card = currentCard;
        text.text = $"Play {currentCard.abilityName} for {currentCard.abilityCost}?";
        
    }

    public void SetAbilityInfo()
    {
        text.text = card.ability.Description + "\n" + defaultText;
    }

    public void PlayingAbility()
    {
        // toggle hand on
        hand.GetComponent<HandManager>().ToggleActivateHand(true);
        playField.GetComponent<PlayFieldManager>().ToggleActivatePlayfield(true);

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

    public void SetText(string newText)
    {
        Debug.Log("yippee");
        text.text = newText;
    }
    
}
