using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "submissive", menuName = "Data/Trait/Submissive")]
public class SubTrait : Trait
{
    public override void OnDefense(ref int damages, Succubus current, Succubus other, int stack)
    {
        if (damages < 0)   //No effect on healing
            return;

        damages /= 2;
        current.Traits.AddTrait(this, -1);

        /* Old Sub was acting like a shield
         
        if (stack >= damages)
        {
            current.Traits.AddTrait(this, -damages);
            damages = 0;
        }
        else
        {
            damages -= stack;
            current.Traits.RemoveTrait(this);
        }*/
    }
}
