using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "submissive", menuName = "Data/Trait/Submissive")]
public class SubTrait : Trait
{
    public override void OnDefense(ref int damages, Character current, Character other, int stack)
    {
        if (damages <= 0)   //No effect on healing
            return;

        if(stack >= damages)
        {
            current.Traits.AddTrait(this, -damages);
            damages = 0;
        }
        else
        {
            damages -= stack;
            current.Traits.RemoveTrait(this);
        }
    }
}
