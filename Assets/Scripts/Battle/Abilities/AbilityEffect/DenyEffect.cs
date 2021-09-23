using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenyEffect : AbilityEffect
{
    public DenyEffect() { }
    public DenyEffect(int bonus, bool usesDice, float mult, bool targetsUser)
        : base(bonus, usesDice, mult, targetsUser)
    { }

    public override void Play(int dice)
    {
        List<Ability> abilities = GameManager.Instance.BattleManager.Abilities(targetsUser);

        Ability abi = abilities[Random.Range(0, abilities.Count)];
        //abi.Lock = Value(dice);
    }
}
