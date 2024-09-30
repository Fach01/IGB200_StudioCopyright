using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farsight : BuilderAbility
{
    public override string Description { set { value = desc; } get { return desc; } }
    public string desc = "look at the top 4 cards in your deck and replace a card in your hand with one of them";

    public override void ActivateAbility(PlayerManager playerManager, GameObject AbilityUI)
    {
        Debug.Log("Wooowww foundation");
    }
}
