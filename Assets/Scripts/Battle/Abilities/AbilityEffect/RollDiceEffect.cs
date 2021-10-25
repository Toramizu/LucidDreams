using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDiceEffect : AbilityEffect
{
    [SerializeField] ConditionData rollCondition;

    public RollDiceEffect() { }
    public RollDiceEffect(EffectData data) : base(data)
    {
        //data.Condition is for conditional effects
    }
    
    public override void Play(Character user, Character other, int dice, Ability abi)
    {
        Debug.Log("Playing? " + dice);
        if(targetsUser)
            user.Roll(Value(dice, abi), rollCondition.ToCondition());
        else
            other.Roll(Value(dice, abi), rollCondition.ToCondition());
    }

    public override AbilityEffect Clone()
    {
        RollDiceEffect e = new RollDiceEffect();
        e.rollCondition = rollCondition;
        return e;
    }
}
