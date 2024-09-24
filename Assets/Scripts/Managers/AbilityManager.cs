using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private List<IAbility> abilities = new List<IAbility>();

    void Awake()
    {
        foreach (Transform child in this.transform)
        {
            IAbility ability = child.GetComponent<IAbility>();
            if (ability != null)
            {
                abilities.Add(ability);
                Debug.Log(ability);
            }
        }
    }

    public IAbility AssignAbility(string abilityName)
    {
        foreach (IAbility ability in abilities)
        {
            if(ability.GetType().Name.ToLower() == abilityName.ToLower())
            {
                return ability;
            }
        }
        Debug.Log("Could not find Ability");
        return null;
    }

}
