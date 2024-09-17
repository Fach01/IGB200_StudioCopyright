using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuilderAbility : MonoBehaviour, IAbility
{
    public abstract string Description { get; set; }

    public abstract void ActivateAbility(PlayerManager playerManager);
}
