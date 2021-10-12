using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : AbilityEffect
{
    protected override float AIValue { get { return 10f; } }

    public DamageEffect() { }
    /*public DamageEffect(int bonus, bool usesDice, bool usesCumulativeBonus, float mult, bool targetsUser, DiceCondition condition) 
        : base(bonus, usesDice, usesCumulativeBonus, mult, targetsUser, condition)
    { }*/
    public DamageEffect(EffectData data) : base(data)
    { }

    public override void Play(int dice, Ability abi)
    {
        GameManager.Instance.BattleManager.InflictsDamage(Value(dice, abi), targetsUser, false);
    }

    public override void GetAIValue(int dice, AIData current, Ability abi)
    {
        if (condition != null && !condition.Check(dice))
            return ;

        current.InflictDamage(Value(dice, abi), targetsUser);

        /*Character user = current.User;


        Character target;
        if (targetsUser)
            target = current.User;
        else
            target = current.Target;

        int amount = Value(dice, 0);

        int total = GameManager.Instance.BattleManager.CalculateDamages(amount, user, target, false);


        if (targetsUser)
            current.UserHP -= total;
        else
            current.TargetHP -= total;


        return AIValue * total;*/
    }
}
