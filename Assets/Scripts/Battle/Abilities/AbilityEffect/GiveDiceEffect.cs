using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDiceEffect : AbilityEffect
{
    [SerializeField] int amount;
    
    public GiveDiceEffect() { }
    public GiveDiceEffect(EffectData data) : base(data)
    { }

    public override void Play(Succubus user, Succubus other, int dice, Ability abi)
    {
        if (targetsUser)
            user.Give(Value(dice, abi));
        else
            other.Give(Value(dice, abi));
    }

    public override AbilityEffect Clone()
    {
        GiveDiceEffect e = new GiveDiceEffect();
        e.amount = amount;
        return e;
    }
}
