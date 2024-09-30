using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flush : BuilderAbility
{
    public override string Description {set{value = desc;} get{return desc;}}
    public string desc = "Discard 1 card, draw 2";

public override void ActivateAbility(PlayerManager playerManager, GameObject AbilityUI)
    {
        playerManager.playField.GetComponent<Button>().interactable = false;
        int numDraw = 2;
        StartCoroutine(SelectCard(playerManager, numDraw, AbilityUI, false));
    }


    //TODO check whether the card being played is still activated and if thats the issue
    IEnumerator SelectCard(PlayerManager playerManager, int numDraw, GameObject AbilityUI, bool confirmed)
    {
        while (!confirmed)
        {
            if (playerManager.selectedCard == null || !playerManager.hand.GetComponent<HandManager>().SearchForCard(playerManager.selectedCard))
            {
                yield return null;
            }
            else {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    confirmed = true;
                } 
            }
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
            AbilityUI.GetComponent<AbilityUI>().Reset();

            yield break;
        }
    }
}
