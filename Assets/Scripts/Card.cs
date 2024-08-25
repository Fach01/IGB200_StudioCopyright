 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public int cost; // cost to play the card
    public bool planner; // is card regular or planner
    public string name; // Manager, Councl Permits, Bricklayer, Electrician etc.
    public string description;
    public int utilities; // amount of utilities recieved from playing the card
    public int framework;
    // public Ability ability // ability script, can be passive or activated



}
