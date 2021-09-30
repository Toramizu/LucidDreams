using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDiceEffect : AbilityEffect
{
    [SerializeField] ConditionData rollCondition;
    [SerializeField] int amount;

    public RollDiceEffect() { }
    public RollDiceEffect(int bonus, bool usesDice, float mult, bool targetsUser, DiceCondition condition)
        : base(bonus, usesDice, mult, targetsUser, condition)
    { }

    public override void Play(int dice)
    {
        GameManager.Instance.BattleManager.Roll(Value(dice), false, rollCondition.ToCondition());
    }
}
