using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farsight : BuilderAbility
{
    public override string Description { set { value = desc; } get { return desc; } }
    public string desc = "look at the top 4 cards in your deck and replace a card in your hand with one of them";

    public GameObject deckSnapshot;

    public override void ActivateAbility(PlayerManager playerManager, GameObject AbilityUI)
    {
        deckSnapshot.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            //draw a card
            //set it in a slot in deck snapshot
            //make sure no other cards are clickable
            //start a coroutine while you wait to select a card to add to your hand, and a card from hand to replace
            //once selection confirmed, add card objects of the 3 unchosen cards, and the card in the hand back into the deck
            //then destroy their gameobjects
        }
    }
}
