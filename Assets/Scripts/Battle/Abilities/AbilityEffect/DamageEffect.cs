using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : AbilityEffect
{
    protected override float AIValue { get { return 10f; } }

    public DamageEffect() { }
    public DamageEffect(EffectData data) : base(data)
    { }

    public override void Play(Character user, Character other, int dice, Ability abi)
    {
        if (targetsUser)
            user.InflictDamage(
                CalculateDamages(Value(dice, abi), user, user)
                );
        else
            other.InflictDamage(
                CalculateDamages(Value(dice, abi), user, other)
                );
    }

    int CalculateDamages(int amount, Character user, Character target)
    {
        user.Traits.OnAttack(ref amount, user, target);
        target.Traits.OnDefense(ref amount, target, user);

        return amount;
    }

    public override AbilityEffect Clone()
    {
        return new DamageEffect();
    }
}
