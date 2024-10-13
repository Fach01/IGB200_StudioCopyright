using UnityEngine;

public interface IAbility
{

    public string Description(int level);

    public void ActivateAbility(PlayerManager playerManager, GameObject AbilityUI, int level);
}


