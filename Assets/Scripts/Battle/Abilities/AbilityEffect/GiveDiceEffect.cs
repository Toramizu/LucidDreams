using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDiceEffect : AbilityEffect
{
    [SerializeField] int amount;
    
    public GiveDiceEffect() { }
    public GiveDiceEffect(int bonus, bool usesDice, float mult, bool targetsUser, DiceCondition condition)
        : base(bonus, usesDice, mult, targetsUser, condition)
    { }

    public override void Play(int dice)
    {
        GameManager.Instance.BattleManager.Give(Value(dice));
    }
}
