using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuilderAbility : MonoBehaviour, IAbility
{
    public abstract string Description(int level);

    public abstract void ActivateAbility(PlayerManager playerManager, GameObject AbilityUI, int level);
}
