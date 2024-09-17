using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flush : BuilderAbility
{
    public override void ActivateAbility(PlayerManager playerManager)
    {
        playerManager.playField.GetComponent<Button>().interactable = false;
        GameObject cardChosen = null;
        int numDraw = 2;
        StartCoroutine(SelectCard(null, playerManager, numDraw));
    }


    //TODO check whether the card being played is still activated and if thats the issue
    private IEnumerator SelectCard(GameObject cardChosen, PlayerManager playerManager, int numDraw)
    {
        Debug.Log("Flush is active");
        if (cardChosen == null)
        {
            if (playerManager.selectedCard != null ) // && card is in hand
            {
                cardChosen = playerManager.selectedCard;
                playerManager.DiscardCard();

                for (int i = 0; i < numDraw; i++)
                {
                    playerManager.deck.GetComponent<DeckManager>().DrawCard();
                }
                playerManager.playField.GetComponent<Button>().interactable = true;
            }
        }
        yield return null;
    }
}
