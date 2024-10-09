using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flush : BuilderAbility
{
    public override string Description {set{value = desc;} get{return desc;}}
    public string desc = "Discard 1 card, draw 2";
    public GameObject deck;

public override void ActivateAbility(PlayerManager playerManager, GameObject AbilityUI)
    {
        DeckManager deckManager = deck.GetComponent<DeckManager>();
        int numDraw = 2;

        if (deckManager != null) {
            if (deckManager.deck.Count <= 0)
            {
                AbilityUI.GetComponent<AbilityUI>().SetText("Deck is empty! Press enter to continue.");
                StartCoroutine(AbilityUI.GetComponent<AbilityUI>().WaitForConfirm(false));
            }
            else if (playerManager.hand.GetComponent<HandManager>().hand.Count <= 0)
            {
                for (int i = 0; i < numDraw; i++)
                {
                    playerManager.DrawCard();
                }
            }
            else
            {
                AbilityUI.GetComponent<AbilityUI>().SetAbilityInfo();
                playerManager.playField.GetComponent<Button>().interactable = false;
                
                StartCoroutine(SelectCard(playerManager, numDraw, AbilityUI, false));
            }
        } 
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
                playerManager.DrawCard();
            }

            playerManager.playField.GetComponent<Button>().interactable = true;
            AbilityUI.GetComponent<AbilityUI>().Reset();

            yield break;
        }
    }
}
