using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dodge", menuName = "Data/Trait/Dodge")]
public class DodgeTrait : Trait
{
    public override void OnDefense(ref int damages, Character current, Character other)
    {
        if (damages <= 0)   //No effect on healing
            return;

        damages = 0;
        current.Traits.AddTrait(this, -1);
    }
}
