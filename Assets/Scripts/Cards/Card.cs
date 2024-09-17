using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public int cost; // cost to play the card
    public Sprite image; // image of the card
    public string name; // Manager, Councl Permits, Bricklayer, Electrician etc.
    public CardType type;
    public string description;
    public int utilities; // amount of utilities recieved from playing the card
    public int frameworks;
    public IAbility ability; // ability script, can be passive or activated

}
