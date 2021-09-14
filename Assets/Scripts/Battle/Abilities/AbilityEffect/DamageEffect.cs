using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : AbilityEffect
{
    public DamageEffect() { }
    public DamageEffect(int bonus, bool usesDice, float mult, bool targetsUser) 
        : base(bonus, usesDice, mult, targetsUser)
    { }

    public override void Play(bool isOpponent, int dice)
    {
        int dmg = bonus;
        if(usesDice)
            dmg += (int) (dice * multiplier);

        GameManager.Instance.BattleManager.InflictsDamage(dmg, isOpponent == targetsUser, isOpponent);

        GameManager.Instance.BattleManager.CheckBattleStatus();
    }
}
