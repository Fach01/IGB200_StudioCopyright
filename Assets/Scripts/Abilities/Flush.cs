using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private bool IsArrayFull(GameObject[] array)
    {
        return array.All(slot => slot != null);
    }


    //TODO check whether the card being played is still activated and if thats the issue
    IEnumerator SelectCard(PlayerManager playerManager, int numDraw, int numDisc, GameObject AbilityUI, bool confirmed)
    {
        GameObject[] discardCards = new GameObject[numDisc];
        while (!confirmed)
        {
            if (IsArrayFull(discardCards))
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    confirmed = true;
                }
            }
        
            if (playerManager.selectedCard != null)
            {
                if (!IsArrayFull(discardCards))
                {
                    for (int i = 0; i < numDisc; i++)
                    {
                        if (discardCards[i] == null)
                        {
                            discardCards[i] = playerManager.selectedCard;
                            break;
                        }
                    }
                }
                else
                {
                    // move all cards forward one slot
                    for (int i = 1; i < numDisc; i++)
                    {
                        discardCards[i - 1] = discardCards[i];
                    }

                    // add new card in last slot
                    discardCards[numDisc - 1] = playerManager.selectedCard;
                }
            }
            yield return null;
        }

        foreach (GameObject card in discardCards)
        {
            if (card != null)
            {
                Destroy(card);
            }
        }

        //playerManager.selectedCard.GetComponent<CardManager>().cardanimator.SetBool("Flush", true);

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
