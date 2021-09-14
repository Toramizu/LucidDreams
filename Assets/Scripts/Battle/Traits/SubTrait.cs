using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "submissive", menuName = "Data/Trait/Submissive", order = 5)]
public class SubTrait : Trait
{
    public override void OnDefense(ref int damages, Character current, Character other)
    {
        if (damages <= 0)   //No effect on healing
            return;

        damages -= current.Traits.TraitStack(this);
        if (damages <= 0)
            damages = 0;
        current.Traits.AddTrait(this, -1);
    }

    public override void StartTurn(Character current)
    {
        current.Traits.RemoveTrait(this);
    }
}
