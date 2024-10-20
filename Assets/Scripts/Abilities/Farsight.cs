using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farsight : BuilderAbility
{
    public override string Description(int level)
    {
        switch (level)
        {
            case 1:
                return "View 2 cards in your deck and swap one with a card in your hand";
            case 2:
                return "View 3 cards in your deck and swap one with a card in your hand";
            case 3:
                return "View 4 cards in your deck and swap one with a card in your hand";
            default:
                return "View 2 cards in your deck and swap one with a card in your hand";
        }
    }



    public GameObject deckSnapshot;
    public GameObject deck;

    public override void ActivateAbility(PlayerManager playerManager, GameObject AbilityUI, int level)
    {
        int numSnapshot;
        switch (level)
        {
            case 1:
                numSnapshot = 2;
                break;
            case 2:
                numSnapshot = 3;
                break;
            case 3:
                numSnapshot = 4;
                break;
            default:
                numSnapshot = 2;
                break;
        }
        DeckManager deckManager = deck.GetComponent<DeckManager>();

        if (deckManager != null)
        {
            if (deckManager.deck.Count <= 0)
            {
                AbilityUI.GetComponent<AbilityUI>().SetText("Deck is empty! Press enter to continue.");
                StartCoroutine(AbilityUI.GetComponent<AbilityUI>().WaitForConfirm(false));
            }
            else
            {
                AbilityUI.GetComponent<AbilityUI>().SetAbilityInfo();
                deckSnapshot.SetActive(true);
                playerManager.playField.GetComponent<Button>().interactable = false;
                DeckSnapshot snapshotManager = deckSnapshot.GetComponent<DeckSnapshot>();

                for (int i = 0; i < numSnapshot; i++)
                {
                    snapshotManager.AddCard();
                }

                StartCoroutine(SwitchCards(playerManager, false, null, null, AbilityUI, snapshotManager));
            }
            
        }
 
    }

    IEnumerator SwitchCards(PlayerManager playerManager, bool confirmed, GameObject chosenHandCard, GameObject chosenDeckCard, GameObject AbilityUI, DeckSnapshot snapshotManager)
    {
        while (!confirmed)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (chosenHandCard != null && chosenDeckCard != null)
                {
                    confirmed = true;
                }
                else
                {
                    Debug.Log("not all cards selected!");
                }
            }

            if (playerManager.selectedCard != null)
            {
                if (playerManager.hand.GetComponent<HandManager>().SearchForCard(playerManager.selectedCard))
                {
                    if (chosenHandCard != null && chosenHandCard != playerManager.selectedCard)
                    {
                        chosenHandCard.GetComponent<CardManager>().Unlock();
                        chosenHandCard.GetComponent<CardManager>().Deselect();
                    }

                    chosenHandCard = playerManager.selectedCard;
                    playerManager.selectedCard.GetComponent<CardManager>().locked = true;
                }
                else
                {
                    if (chosenDeckCard != null && chosenDeckCard != playerManager.selectedCard)
                    {
                        chosenDeckCard.GetComponent<CardManager>().Unlock();
                        chosenDeckCard.GetComponent<CardManager>().Deselect();                    
                    }
                    
                    chosenDeckCard = playerManager.selectedCard;
                    playerManager.selectedCard.GetComponent<CardManager>().locked = true;
                }
            }
            yield return null;
        }

        // move chosen card to hand
        AudioManager.instance.PlaySFX("Farsight");
        chosenDeckCard.GetComponent<CardManager>().Unlock();
        chosenDeckCard.GetComponent<CardManager>().Deselect();
        

        snapshotManager.cards.Remove(chosenDeckCard);
        playerManager.hand.GetComponent<HandManager>().AddCard(chosenDeckCard);
        
        // return all other cards to deck
        snapshotManager.cards.Add(chosenHandCard);
        snapshotManager.ReturnCards();

        //once selection confirmed, add card objects of the 3 unchosen cards, and the card in the hand back into the deck
        //then destroy their gameobjects

        playerManager.playField.GetComponent<Button>().interactable = true;
        AbilityUI.GetComponent<AbilityUI>().Reset(true);

        yield break;
    }
}
