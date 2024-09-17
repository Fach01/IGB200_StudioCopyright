using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flush : BuilderAbility
{
    public override void ActivateAbility(PlayerManager playerManager)
    {
        GameObject cardChosen = null;
        int numDraw = 2;
        StartCoroutine(SelectCard(null, playerManager, numDraw));
    }

    private IEnumerator SelectCard(GameObject cardChosen, PlayerManager playerManager, int numDraw)
    {
        while (cardChosen == null)
        {
            if (playerManager.selectedCard != null)
            {
                if (Input.GetKeyDown("Enter"))
                {
                    cardChosen = playerManager.selectedCard;
                    playerManager.DiscardCard();

                    for (int i = 0; i < numDraw; i++) {
                        playerManager.deck.GetComponent<DeckManager>().DrawCard();
                    }
                }
                yield return null;
            }
        }
    }
}
