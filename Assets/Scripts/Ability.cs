using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public int cost;
    public string description;

    public abstract void ActivateAbility();
}

[CreateAssetMenu(menuName = "Abilities/Surveyor")]
public class Surveyor : Ability
{
    private void OnEnable()
    {
        cost = 0;
        description = "For every Framework card you play, double the output.";
    }
    public override void ActivateAbility()
    {
        throw new System.NotImplementedException();

    }
}
