using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenyEffect : AbilityEffect
{
    protected override float AIValue { get { return 8f; } }

    public DenyEffect() { }
    public DenyEffect(int bonus, bool usesDice, float mult, bool targetsUser, DiceCondition condition)
        : base(bonus, usesDice, mult, targetsUser, condition)
    { }

    public override void Play(int dice)
    {
        List<Ability> abilities = GameManager.Instance.BattleManager.Abilities(targetsUser);

        Ability abi = abilities[Random.Range(0, abilities.Count)];
        //abi.Lock = Value(dice);
    }
}
