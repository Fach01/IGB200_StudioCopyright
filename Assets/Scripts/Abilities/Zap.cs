using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Zap : BuilderAbility
{
    public override string Description { set { value = desc; } get { return desc; } }
    public string desc = "wow!";

    public override void ActivateAbility(PlayerManager playerManager)
    {
        Debug.Log("Wooowww foundation");
    }


    IEnumerator SelectCards(PlayerManager playerManager, bool confirmed, GameObject chosenHandCard, GameObject chosenPlayedCard)
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


        //adjust UI stuff

        yield break;
    }
}