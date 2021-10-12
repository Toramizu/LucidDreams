using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dominant", menuName = "Data/Trait/Dominant", order = 5)]
public class DomTrait : Trait
{
    public override void OnAttack(ref int damages, Character current, Character other, int stack)
    {
        if (damages <= 0)   //No effect on healing
            return;

        damages += stack;
    }

    public override void EndTurn(Character current, int stack)
    {
        current.Traits.RemoveTrait(this);
    }
}
