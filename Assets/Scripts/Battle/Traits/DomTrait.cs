using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dominant", menuName = "Data/Trait/Dominant", order = 5)]
public class DomTrait : Trait
{
    public override void OnAttack(ref int damages, Character current, Character other)
    {
        if (damages <= 0)   //No effect on healing
            return;

        damages += current.Traits.TraitStack(this);
        current.Traits.AddTrait(this, -1);
    }

    public override void StartTurn(Character current)
    {
        current.Traits.RemoveTrait(this);
    }
}