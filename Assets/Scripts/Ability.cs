using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public int cost;
    public string description;

    public abstract void ActivateAbility();
}


