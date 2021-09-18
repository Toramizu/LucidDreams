using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "submissive", menuName = "Data/Trait/Submissive")]
public class SubTrait : Trait
{
    public override void OnDefense(ref int damages, Character current, Character other)
    {
        if (damages <= 0)   //No effect on healing
            return;

        /*damages -= current.Traits.TraitStack(this);
        if (damages <= 0)
            damages = 0;
        current.Traits.AddTrait(this, -1);*/

        int sub = current.Traits.TraitStack(this);

        if(sub >= damages)
        {
            current.Traits.AddTrait(this, -damages);
            damages = 0;
        }
        else
        {
            damages -= sub;
            current.Traits.RemoveTrait(this);
        }
    }
}
