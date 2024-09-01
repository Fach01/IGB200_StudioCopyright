using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public int cost;

    public abstract void ActivateAbility();
}
