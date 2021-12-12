using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dominant", menuName = "Data/Trait/Dominant", order = 5)]
public class DomTrait : Trait
{
    public override void OnAttack(ref int damages, Succubus current, Succubus other, int stack)
    {
        if (damages < 0)   //No effect on healing
            return;

        damages *= 2;

        /*Old Dom was acting like a 1 to 1 damage boost
         
    damages += stack;
    }

    public override void EndTurn(Succubus current, int stack)
    {
        current.Traits.RemoveTrait(this);*/
    }
}
