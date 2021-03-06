using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class RollDiceEffect : AbilityEffect
{
    [XmlIgnore]
    ConditionData rollCondition;

    public RollDiceEffect() { }
    public RollDiceEffect(EffectData data) : base(data)
    {
        //data.Condition is for conditional effects
    }
    
    public override void Play(Succubus user, Succubus other, int dice, Ability abi)
    {
        DiceCondition cond;
        if (rollCondition == null)
            cond = null;
        else
            cond = rollCondition.ToCondition();

        if (TargetsUser)
            user.Roll(Value(dice, abi), cond);
        else
            other.Roll(Value(dice, abi), cond);
    }

    public override AbilityEffect Clone()
    {
        RollDiceEffect e = new RollDiceEffect();
        e.rollCondition = rollCondition;
        return e;
    }
}
