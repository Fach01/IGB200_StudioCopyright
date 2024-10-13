using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flush : BuilderAbility
{
    public override string Description(int level)
    {
        switch (level)
        {
            case 1:
                return "Discard 1 card, draw 2";
            case 2:
                return "Discard 2 cards, draw 2";
            default:
                return "Discard 1 card, draw 2";

        }
    }
    public GameObject deck;

public override void ActivateAbility(PlayerManager playerManager, GameObject AbilityUI, int level)
    {
        DeckManager deckManager = deck.GetComponent<DeckManager>();
        int numDraw;
        int numDisc;


        switch (level)
        {
            case 1:
                numDisc = 1;
                numDraw = 2;
                break;
            case 2:
                numDisc = 2;
                numDraw = 2;
                break;
            default:
                numDisc = 1;
                numDraw = 2;
                break;
        }

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
                
                StartCoroutine(SelectCard(playerManager, numDraw, numDisc, AbilityUI, false));
            }
        } 
    }

    

    //TODO check whether the card being played is still activated and if thats the issue
    IEnumerator SelectCard(PlayerManager playerManager, int numDraw, int numDisc, GameObject AbilityUI, bool confirmed)
    {
        GameObject[] discardCards = new GameObject[numDisc];
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
            playerManager.selectedCard.GetComponent<CardManager>().cardanimator.SetBool("Flush", true);

            AudioManager.instance.PlaySFX("Flush");
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
