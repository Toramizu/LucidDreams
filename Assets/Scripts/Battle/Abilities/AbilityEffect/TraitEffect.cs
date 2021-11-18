using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitEffect : AbilityEffect
{
    Trait trait;

    protected override float AIValue { get { return trait.AIValue; } }

    public TraitEffect() { }
    public TraitEffect(EffectData data) : base(data)
    {
        trait = GameManager.Instance.Assets.Traits[data.StringValue];
    }

    public override void Play(Succubus user, Succubus other, int dice, Ability abi)
    {
        if (targetsUser)
            user.Traits.AddTrait(trait, Value(dice, abi));
        else
            other.Traits.AddTrait(trait, Value(dice, abi));
    }

    public override AbilityEffect Clone()
    {
        TraitEffect e = new TraitEffect();
        e.trait = trait;
        return e;
    }
}
