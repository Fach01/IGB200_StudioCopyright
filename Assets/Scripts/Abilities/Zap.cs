using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Zap : BuilderAbility
{
    public override string Description { set { value = desc; } get { return desc; } }
    public string desc = "wow!";

    public override void ActivateAbility(PlayerManager playerManager, GameObject AbilityUI)
    {
        AbilityUI.GetComponent<AbilityUI>().SetAbilityInfo();
        if (playerManager.playField.GetComponent<PlayFieldManager>().cards.Count == 0)
        {
            // cant complete this - handle
        }
        playerManager.playField.GetComponent<Button>().interactable = false;
        StartCoroutine(SelectCards(playerManager, false, null, null, AbilityUI));
    }


    IEnumerator SelectCards(PlayerManager playerManager, bool confirmed, GameObject chosenHandCard, GameObject chosenPlayedCard, GameObject AbilityUI)
    {
        // select one card from hand and one from playfield, then swap them

        while (!confirmed) {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (chosenHandCard != null && chosenPlayedCard != null)
                {
                    confirmed = true;
                }
                else
                {
                    Debug.Log("not all cards selected!");
                }
            }
            
            if (playerManager.selectedCard != null) {
                if (playerManager.hand.GetComponent<HandManager>().SearchForCard(playerManager.selectedCard)){
                    chosenHandCard = playerManager.selectedCard;
                    //TODO: make sure it stays 'selected'
                }
                else
                {
                    chosenPlayedCard = playerManager.selectedCard;
                    //TODO: make sure it stays 'selected'
                }
            }
            yield return null;
        }

        //add card from hand to playfield
        playerManager.playField.GetComponent<PlayFieldManager>().AddCard(chosenHandCard);
        //add card from playfield to hand
        playerManager.playField.GetComponent<PlayFieldManager>().MoveCardToHand(chosenPlayedCard);

        playerManager.playField.GetComponent<PlayFieldManager>().OrderCards();


        playerManager.playField.GetComponent<Button>().interactable = true;
        AbilityUI.GetComponent<AbilityUI>().Reset();

        yield break;
    }
}