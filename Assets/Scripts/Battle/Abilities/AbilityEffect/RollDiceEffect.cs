using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDiceEffect : AbilityEffect
{
    [SerializeField] ConditionData condition;
    [SerializeField] int amount;

    protected override float AIValue { get { return 8f; } }

    public RollDiceEffect() { }
    public RollDiceEffect(int bonus, bool usesDice, float mult, bool targetsUser)
        : base(bonus, usesDice, mult, targetsUser)
    { }

    public override void Play(int dice)
    {
        GameManager.Instance.BattleManager.Roll(Value(dice), false, condition.ToCondition());
    }
}
