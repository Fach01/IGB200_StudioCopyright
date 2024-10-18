using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Foundation : BuilderAbility
{
    public override string Description(int level)
    {
        switch (level)
        {
            case 1:
                return "Play an extra card for no additional cost.";
            case 2:
                return "Play 2 extra cards for no additional cost.";
            default:
                return "Play an extra card for no additional cost.";
        }
    }

    public override void ActivateAbility(PlayerManager playerManager, GameObject AbilityUI, int level)
    {
        
        if (playerManager.hand.GetComponent<HandManager>().hand.Count <= 0)
        {
            AbilityUI.GetComponent<AbilityUI>().SetText("Hand is empty! Press enter to continue.");
            StartCoroutine(AbilityUI.GetComponent<AbilityUI>().WaitForConfirm(false));
        } else
        {
            int numPlay;
            switch (level)
            {
                case 1:
                    numPlay = 1;
                    break;
                case 2:
                    numPlay = 2;
                    break;
                default:
                    numPlay = 1;
                    break;
            }

            AbilityUI.GetComponent<AbilityUI>().SetAbilityInfo();
            playerManager.playField.GetComponent<Button>().interactable = false;
            StartCoroutine(FreePlay(playerManager, false, null, AbilityUI, numPlay));
        } 
    }

    private bool IsArrayFull(GameObject[] array)
    {
        return array.All(slot => slot != null);
    }

    IEnumerator FreePlay(PlayerManager playerManager, bool confirmed, GameObject chosenCard, GameObject AbilityUI, int numPlay)
    {
        GameObject[] cardsToPlay = new GameObject[numPlay];

        while (!confirmed)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (IsArrayFull(cardsToPlay))
                {
                    confirmed = true;
                }
                else
                {
                    Debug.Log("Select cards!");
                }
            }

            if (playerManager.selectedCard != null)
            {
                if (playerManager.hand.GetComponent<HandManager>().SearchForCard(playerManager.selectedCard))
                {

                    bool isSelected = false;
                    foreach (GameObject card in cardsToPlay)
                    {
                        if (card != null)
                        {
                            if (card == playerManager.selectedCard)
                            {
                                isSelected = true;
                                break;
                            }
                        }
                    }

                        if (!IsArrayFull(cardsToPlay) && !isSelected)
                    {
                        for (int i = 0; i < numPlay; i++)
                        {
                            if (cardsToPlay[i] == null)
                            {
                                cardsToPlay[i] = playerManager.selectedCard;
                                playerManager.selectedCard.GetComponent<CardManager>().locked = true;
                                break;
                            }
                        }
                    }
                    else if (IsArrayFull(cardsToPlay) && !isSelected)
                        {
                        cardsToPlay[0].GetComponent<CardManager>().Unlock();
                        cardsToPlay[0].GetComponent<CardManager>().Deselect();
                        // move all cards forward one slot
                        for (int i = 1; i < numPlay; i++)
                        {
                            cardsToPlay[i - 1] = cardsToPlay[i];
                        }

                        // add new card in last slot
                        cardsToPlay[numPlay - 1] = playerManager.selectedCard;
                        playerManager.selectedCard.GetComponent<CardManager>().locked = true;
                    }
                }
            }
            yield return null;
        }

        //add card from hand to playfield
        AudioManager.instance.PlaySFX("Foundation");

        foreach (GameObject card in cardsToPlay)
        {
            if (card != null)
            {
                playerManager.playField.GetComponent<PlayFieldManager>().AddCard(card);
            }
        }
        
        playerManager.playField.GetComponent<PlayFieldManager>().OrderCards();

        playerManager.playField.GetComponent<Button>().interactable = true;
        AbilityUI.GetComponent<AbilityUI>().Reset();

        yield break;
    }
}
