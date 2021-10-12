using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDiceEffect : AbilityEffect
{
    [SerializeField] ConditionData rollCondition;
    [SerializeField] int amount;

    public RollDiceEffect() { }
    /*public RollDiceEffect(int bonus, bool usesDice, bool usesCumulativeBonus, float mult, bool targetsUser, DiceCondition condition)
        : base(bonus, usesDice, usesCumulativeBonus, mult, targetsUser, condition)
    { }*/
    public RollDiceEffect(EffectData data) : base(data)
    { }

    public override void Play(int dice, Ability abi)
    {
        GameManager.Instance.BattleManager.Roll(Value(dice, abi), false, rollCondition.ToCondition());
    }
}
