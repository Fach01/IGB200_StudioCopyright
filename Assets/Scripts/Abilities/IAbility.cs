using UnityEngine;

public interface IAbility
{
    public int Cost { get; set; }
    public string Description { get; set; }

    public void ActivateAbility(PlayerManager playerManager);
}


