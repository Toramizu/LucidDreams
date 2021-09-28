using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : AbilityEffect
{
    protected override float AIValue { get { return 10f; } }

    public DamageEffect() { }
    public DamageEffect(int bonus, bool usesDice, float mult, bool targetsUser) 
        : base(bonus, usesDice, mult, targetsUser)
    { }

    public override void Play(int dice)
    {
        GameManager.Instance.BattleManager.InflictsDamage(Value(dice), targetsUser, false);
    }

    public override float GetAIValue(int dice, AIData current)
    {
        Character user = current.User;


        Character target;
        if (targetsUser)
            target = current.User;
        else
            target = current.Target;

        int amount = Value(dice);

        int total = GameManager.Instance.BattleManager.CalculateDamages(amount, user, target, false);


        if (targetsUser)
            current.UserHP -= Value(dice);
        else
            current.TargetHP -= Value(dice);


        return AIValue * total;
    }
}
