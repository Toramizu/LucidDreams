using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : AbilityEffect
{
    public DamageEffect() { }
    public DamageEffect(EffectData data) : base(data)
    { }

    public override void Play(Succubus user, Succubus other, int dice, Ability abi)
    {
        if (TargetsUser)
            user.InflictDamage(
                CalculateDamages(Value(dice, abi), user, user)
                );
        else
            other.InflictDamage(
                CalculateDamages(Value(dice, abi), user, other)
                );
    }

    int CalculateDamages(int amount, Succubus user, Succubus target)
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
