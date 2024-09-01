using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public int cost;
    public string description;
    public abstract void ActivateAbility();
}
