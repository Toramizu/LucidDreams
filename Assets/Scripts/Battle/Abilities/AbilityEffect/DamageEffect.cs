using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : AbilityEffect
{
    public DamageEffect() { }
    public DamageEffect(int bonus, bool usesDice, float mult, bool targetsUser) 
        : base(bonus, usesDice, mult, targetsUser)
    { }

    public override void Play(int dice)
    {
        GameManager.Instance.BattleManager.InflictsDamage(Value(dice), targetsUser, false);
    }
}
