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
    private GameObject cardObject = null;

    private string defaultText = "Press enter to confirm your choice.";

    public void OnPlayAbility()
    {
        if (card != null && card.ability != null)
        {
            card.ability.ActivateAbility(player.GetComponent<PlayerManager>(), this.gameObject, card.abilityLevel);
            PlayingAbility();
        }
        else
        {
            Debug.Log("oh no");
            cardObject.GetComponent<CardManager>().ScaleCard();
            cardObject.GetComponent<CardManager>().Unlock();
            playField.GetComponent<PlayFieldManager>().OrderCards();
        }
    }

    public void OnPlayCard()
    {
        cardObject.GetComponent<CardManager>().Unlock();
        cardObject.GetComponent<CardManager>().ScaleCard();

        // toggle cards on
        hand.GetComponent<HandManager>().ToggleActivateHand(true);
        playField.GetComponent<PlayFieldManager>().ToggleActivatePlayfield(true);

        player.GetComponent<PlayerManager>().endTurn.GetComponent<Button>().interactable = true;

        playField.GetComponent<PlayFieldManager>().OrderCards();
        this.gameObject.SetActive(false);

    }

    public void SetCard(GameObject currentCard)
    {
        // toggle hand and playfield off
        hand.GetComponent<HandManager>().ToggleActivateHand(false);
        playField.GetComponent<PlayFieldManager>().ToggleActivatePlayfield(false);

        card = currentCard.GetComponent<CardManager>().m_card;
        cardObject = currentCard;
        text.text = $"Play {card.abilityName} for ${card.abilityCost.ToString("N0")}?";
        
    }

    public void SetAbilityInfo()
    {
        text.text = card.ability.Description(card.abilityLevel) + "\n" + defaultText;
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

    public void Reset(bool success)
    {
        if (success)
        {
            player.GetComponent<PlayerManager>().levelManager.GetComponent<LevelManager>().Spend(card.abilityCost);
        }

        player.GetComponent<PlayerManager>().endTurn.GetComponent<Button>().interactable = true;
        playAbilityButton.gameObject.SetActive(true);
        playCardButton.gameObject.SetActive(true);

        cardObject.GetComponent<CardManager>().Unlock();
        playField.GetComponent<PlayFieldManager>().OrderCards();
        this.gameObject.SetActive(false);
    }

    public void SetText(string newText)
    {
        text.text = newText;
    }

    public IEnumerator WaitForConfirm(bool confirmed)
    {
        while (!confirmed)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                confirmed = true;
            }
            yield return null;
        }

        playField.GetComponent<Button>().interactable = true;
        Reset(false);
        yield break;
    }

}
