using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bondage", menuName = "Data/Trait/Bondage")]
public class BondageTrait : Trait
{
    public override void StartTurn(Character current, int stack)
    {
        List<Ability> abilities = new List<Ability>(current.Abilities);

        while(stack > 0 && abilities.Count > 0)
        {
            Ability abi = abilities[Random.Range(0, abilities.Count)];
            abilities.Remove(abi);

            if (abi.IsActive)
            {
                abi.Locked = true;
                stack--;
            }
        }

        /*for(int i = 0; i < amount && abilities.Count > 0; i++)
        {
            Ability abi = abilities[Random.Range(0, abilities.Count)];
            abi.Locked = true;
            abilities.Remove(abi);
        }*/

        current.Traits.RemoveTrait(this);
    }
}
