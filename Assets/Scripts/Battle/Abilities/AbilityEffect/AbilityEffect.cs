using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityEffect
{
    protected int bonus;
    protected bool usesDice;
    protected float multiplier = 1;

    protected DiceCondition condition;

    protected virtual float AIValue { get { return 0f; } }

    [SerializeField] protected bool targetsUser;

    public AbilityEffect() { }
    public AbilityEffect(int bonus, bool usesDice, float mult, bool targetsUser, DiceCondition condition) {
        this.bonus = bonus;
        this.usesDice = usesDice;
        this.multiplier = mult;
        this.targetsUser = targetsUser;
        this.condition = condition;
    }

    protected int Value(int dice)
    {
        if (usesDice && multiplier == 0)
            multiplier = 1;

        int amount = bonus;
        if (usesDice)
            amount += (int)(dice * multiplier);
        return amount;
    }

    public void CheckAndPlay(int dice)
    {
        if (condition == null || condition.Check(dice))
            Play(dice);
    }

    public abstract void Play(int dice);

    public virtual float GetAIValue(int dice, AIData current)
    {
        if (condition == null || condition.Check(dice))
            return AIValue * Value(dice);
        else
            return 0f;
    }
}