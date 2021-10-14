using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDiceEffect : AbilityEffect
{
    [SerializeField] ConditionData rollCondition;
    [SerializeField] int amount;

    public RollDiceEffect() { }
    public RollDiceEffect(EffectData data) : base(data)
    { }
    
    public override void Play(Character user, Character other, int dice, Ability abi)
    {
        if(targetsUser)
            user.Roll(Value(dice, abi), rollCondition.ToCondition());
        else
            other.Roll(Value(dice, abi), rollCondition.ToCondition());
    }

    public override AbilityEffect Clone()
    {
        RollDiceEffect e = new RollDiceEffect();
        e.rollCondition = rollCondition;
        e.amount = amount;
        return e;
    }
}
