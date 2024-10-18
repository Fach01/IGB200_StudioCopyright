using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Zap : BuilderAbility
{
    public override string Description(int level)
    {
        switch (level)
        {
            case 1:
                return "Swap one card from your hand with one from the jobsite";
            case 2:
                return "Swap two cards from your hand with ones from the jobsite";
            default:
                return "Swap one card from your hand with one from the jobsite";
        }
    }
   

    public override void ActivateAbility(PlayerManager playerManager, GameObject AbilityUI, int level)
    {
        
        if (playerManager.playField.GetComponent<PlayFieldManager>().Size() <= 1)
        {
            AbilityUI.GetComponent<AbilityUI>().SetText("Jobsite is empty! Press enter to continue.");
            StartCoroutine(AbilityUI.GetComponent<AbilityUI>().WaitForConfirm(false));
        }
        else if (playerManager.hand.GetComponent<HandManager>().hand.Count <= 0)
        {
            AbilityUI.GetComponent<AbilityUI>().SetText("Hand is empty! Press enter to continue.");
            StartCoroutine(AbilityUI.GetComponent<AbilityUI>().WaitForConfirm(false));
        }
        else
        {
            int numSwap;

            switch (level)
            {
                case 1:
                    numSwap = 1;
                    break;
                case 2:
                    numSwap = 2;
                    break;
                default:
                    numSwap = 1;
                    break;
            }

            AudioManager.instance.PlaySFX("Zap");
            AbilityUI.GetComponent<AbilityUI>().SetAbilityInfo();
            playerManager.playField.GetComponent<Button>().interactable = false;
            StartCoroutine(SelectCards(playerManager, false, numSwap, AbilityUI));
        } 
    }

    private bool IsArrayFull(GameObject[] array)
    {
        return array.All(slot => slot != null);
    }

    private void AddCardToArray(GameObject[] cards, GameObject newCard)
    {
        bool isSelected = false;
        foreach (GameObject card in cards)
        {
            if (card != null)
            {
                if (card == newCard)
                {
                    isSelected = true;
                    break;
                }
            }
        }
        if (isSelected)
        {
            return;
        }

        if (!IsArrayFull(cards))
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] == null)
                {
                    newCard.GetComponent<CardManager>().locked = true;
                    cards[i] = newCard;
                    break;
                }
            }
        }
        else
        {
            cards[0].GetComponent<CardManager>().Unlock();
            cards[0].GetComponent<CardManager>().Deselect();
            // move all cards forward one slot
            for (int i = 1; i < cards.Length; i++)
            {
                cards[i - 1] = cards[i];
            }

            // add new card in last slot
            cards[cards.Length - 1] =  newCard;
            newCard.GetComponent<CardManager>().locked = true;
        }

    }
    IEnumerator SelectCards(PlayerManager playerManager, bool confirmed, int numSwap, GameObject AbilityUI)
    {
        // select one card from hand and one from playfield, then swap them

        GameObject[] handCards = new GameObject[numSwap];
        GameObject[] fieldCards = new GameObject[numSwap];

        while (!confirmed) {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (IsArrayFull(handCards) && IsArrayFull(fieldCards))
                {
                    confirmed = true;
                }
                else
                {
                    Debug.Log("not all cards selected!");
                }
            }
            
            if (playerManager.selectedCard != null) {
                if (playerManager.hand.GetComponent<HandManager>().SearchForCard(playerManager.selectedCard)){
                    AddCardToArray(handCards, playerManager.selectedCard);
                    //TODO: make sure it stays 'selected'
                }
                else
                {
                    AddCardToArray(fieldCards, playerManager.selectedCard);
                    //TODO: make sure it stays 'selected'
                }
            }
            yield return null;
        }

        //add cards from hand to playfield
        foreach (GameObject card in handCards)
        {
            if (card != null)
            {
                playerManager.playField.GetComponent<PlayFieldManager>().AddCard(card);
            }
        }

        //add cards from playfield to hand
        foreach (GameObject card in handCards)
        {
            if (card != null)
            {
                playerManager.playField.GetComponent<PlayFieldManager>().MoveCardToHand(card);
            }
        }

        playerManager.playField.GetComponent<PlayFieldManager>().OrderCards();


        playerManager.playField.GetComponent<Button>().interactable = true;
        AbilityUI.GetComponent<AbilityUI>().Reset();

        yield break;
    }
}