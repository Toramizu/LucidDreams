using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unfeeling", menuName = "Data/Trait/Unfeeling")]
public class UnfeelingTrait : Trait
{
    public override void OnDefense(ref int damages, Character current, Character other)
    {
        if (damages <= 0)   //No effect on healing
            return;

        damages = 0;
        current.Traits.AddTrait(this, -1);
    }

    public override void StartTurn(Character current)
    {
        current.Traits.RemoveTrait(this);
    }
}
