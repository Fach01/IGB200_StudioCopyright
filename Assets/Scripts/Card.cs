using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public int cost; // cost to play the card
    public string type; // enum, Framework, Utilities, Planning
    public string name; // Manager, Councl Permits, Bricklayer, Electrician etc.
    public int utilities; // amount of utilities recieved from playing the card
    // public Ability ability // ability script, can be passive or activated

}
