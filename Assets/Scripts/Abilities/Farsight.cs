using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farsight : BuilderAbility
{
    public override string Description { set { value = desc; } get { return desc; } }
    public string desc = "See a snapshot of your deck and switch one with a card from your hand";

    public GameObject deckSnapshot;
    public GameObject deck;

    public override void ActivateAbility(PlayerManager playerManager, GameObject AbilityUI)
    {
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
                DeckSnapshot snapshotManager = deckSnapshot.GetComponent<DeckSnapshot>();

                for (int i = 0; i < 4; i++)
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
                    chosenHandCard = playerManager.selectedCard;
                    //TODO: make sure it stays 'selected'
                }
                else
                {
                    chosenDeckCard = playerManager.selectedCard;
                    //TODO: make sure it stays 'selected'
                }
            }
            yield return null;
        }

        // move chosen card to hand
        snapshotManager.cards.Remove(chosenDeckCard);
        playerManager.hand.GetComponent<HandManager>().AddCard(chosenDeckCard);
        
        // return all other cards to deck
        snapshotManager.cards.Add(chosenHandCard);
        snapshotManager.ReturnCards();

        //once selection confirmed, add card objects of the 3 unchosen cards, and the card in the hand back into the deck
        //then destroy their gameobjects

        playerManager.playField.GetComponent<Button>().interactable = true;
        AbilityUI.GetComponent<AbilityUI>().Reset();

        yield break;
    }
}
