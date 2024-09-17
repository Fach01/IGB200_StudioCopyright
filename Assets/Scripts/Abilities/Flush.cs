using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flush : BuilderAbility
{
    public override void ActivateAbility(PlayerManager playerManager)
    {
        playerManager.playField.GetComponent<Button>().interactable = false;
        int numDraw = 2;
        StartCoroutine(SelectCard(playerManager, numDraw));
    }


    //TODO check whether the card being played is still activated and if thats the issue
    IEnumerator SelectCard(PlayerManager playerManager, int numDraw)
    {
        while (playerManager.selectedCard == null)
        {
            Debug.Log("Waiting...");
            yield return null;
        }

        if (playerManager.selectedCard != null)
        {
            Destroy(playerManager.selectedCard);


            for (int i = 0; i < numDraw; i++)
            {
                playerManager.deck.GetComponent<DeckManager>().DrawCard();
            }
            playerManager.playField.GetComponent<Button>().interactable = true;

            yield break;
        }
    }
}
