using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Foundation : BuilderAbility
{
    public override string Description { set { value = desc; } get { return desc; } }
    public string desc = "Play an extra card for no additional cost.";

    public override void ActivateAbility(PlayerManager playerManager, GameObject AbilityUI)
    {
        playerManager.playField.GetComponent<Button>().interactable = false;
        StartCoroutine(FreePlay(playerManager, false, null, AbilityUI));
    }

    IEnumerator FreePlay(PlayerManager playerManager, bool confirmed, GameObject chosenCard, GameObject AbilityUI)
    {
        // select one card from hand and one from playfield, then swap them

        while (!confirmed)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (chosenCard != null)
                {
                    confirmed = true;
                }
                else
                {
                    Debug.Log("Select a card!");
                }
            }

            if (playerManager.selectedCard != null)
            {
                if (playerManager.hand.GetComponent<HandManager>().SearchForCard(playerManager.selectedCard))
                {
                    chosenCard = playerManager.selectedCard;
                }
            }
            yield return null;
        }

        //add card from hand to playfield
        playerManager.playField.GetComponent<PlayFieldManager>().AddCard(chosenCard);

        playerManager.playField.GetComponent<PlayFieldManager>().OrderCards();


        playerManager.playField.GetComponent<Button>().interactable = true;
        AbilityUI.GetComponent<AbilityUI>().Reset();

        yield break;
    }
}
