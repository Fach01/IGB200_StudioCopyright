using UnityEngine;

public interface IAbility
{

    public string Description { get; set; }

    public void ActivateAbility(PlayerManager playerManager, GameObject AbilityUI);
}


