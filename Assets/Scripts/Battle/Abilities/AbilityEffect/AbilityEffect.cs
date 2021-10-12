using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityEffect
{
    protected int bonus;
    protected bool usesDice;
    protected bool usesCumulativeBonus;
    protected float multiplier = 1;

    protected DiceCondition condition;

    protected virtual float AIValue { get { return 0f; } }

    [SerializeField] protected bool targetsUser;

    public AbilityEffect() { }
    public AbilityEffect(EffectData data)
    {
        bonus = data.Bonus;
        usesDice = data.UsesDice;
        usesCumulativeBonus = data.UsesCumulativeBonus;
        multiplier = data.Multiplier;
        targetsUser = data.TargetsUser;
        condition = data.Condition.ToCondition();
    }

    protected int Value(int dice, Ability abi)
    {
        return Value(dice, abi.Used);
    }

    protected int Value(int dice, int cumulative)
    {
        if (usesDice && multiplier == 0)
            multiplier = 1;

        int amount = bonus;
        if (usesDice)
            amount += (int)(dice * multiplier);
        if (usesCumulativeBonus)
            amount += cumulative;
        return amount;
    }

    public void CheckAndPlay(int dice, Ability abi)
    {
        if (condition == null || condition.Check(dice))
            Play(dice, abi);
    }

    public abstract void Play(int dice, Ability abi);

    public virtual void GetAIValue(int dice, AIData current, Ability abi)
    {
        /*if (condition == null || condition.Check(dice))
            current.AIValue += AIValue * Value(dice, 0);*/
    }
}