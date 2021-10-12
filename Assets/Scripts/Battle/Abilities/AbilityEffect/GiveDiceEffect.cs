using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDiceEffect : AbilityEffect
{
    [SerializeField] int amount;
    
    public GiveDiceEffect() { }
    /*public GiveDiceEffect(int bonus, bool usesDice, bool usesCumulativeBonus, float mult, bool targetsUser, DiceCondition condition)
        : base(bonus, usesDice, usesCumulativeBonus, mult, targetsUser, condition)
    { }*/
    public GiveDiceEffect(EffectData data) : base(data)
    { }

    public override void Play(int dice, Ability abi)
    {
        GameManager.Instance.BattleManager.Give(Value(dice, abi));
    }
}
