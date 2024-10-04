using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public int cost; // cost to play the card
    public Sprite image; // image of the card
    public string name; // Manager, Councl Permits, Bricklayer, Electrician etc.
    public Sprite background;
    public CardType type;
    public string description;
    public int resource;
    public string abilityName;
    public int abilityCost;

    [HideInInspector]
    public IAbility ability; // ability script, can be passive or activated

}
