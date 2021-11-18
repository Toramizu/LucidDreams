using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Frigid", menuName = "Data/Trait/Frigid")]
public class FrigidTrait : Trait
{
    public override void OnDefense(ref int damages, Succubus current, Succubus other, int stack)
    {
        if (damages <= 0)   //No effect on healing
            return;

        damages = 0;
        current.Traits.AddTrait(this, -1);
    }

    public override void StartTurn(Succubus current, int stack)
    {
        current.Traits.RemoveTrait(this);
    }
}
