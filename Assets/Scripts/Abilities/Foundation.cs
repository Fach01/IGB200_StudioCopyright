using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : BuilderAbility
{
    public override string Description { set { value = desc; } get { return desc; } }
    public string desc = "wow!";

    public override void ActivateAbility(PlayerManager playerManager)
    {
        Debug.Log("Wooowww foundation");
    }
}
