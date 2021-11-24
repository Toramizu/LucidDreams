using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDiceEffect : AbilityEffect
{    
    public GiveDiceEffect() { }
    public GiveDiceEffect(EffectData data) : base(data)
    { }

    public override void Play(Succubus user, Succubus other, int dice, Ability abi)
    {
        if (TargetsUser)
            user.Give(Value(dice, abi));
        else
            other.Give(Value(dice, abi));
    }

    public override AbilityEffect Clone()
    {
        GiveDiceEffect e = new GiveDiceEffect();
        return e;
    }
}
