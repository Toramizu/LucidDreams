using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDiceEffect : AbilityEffect
{
    [SerializeField] int amount;

    protected override float AIValue { get { return 8f; } }

    public GiveDiceEffect() { }
    public GiveDiceEffect(int bonus, bool usesDice, float mult, bool targetsUser)
        : base(bonus, usesDice, mult, targetsUser)
    { }

    public override void Play(int dice)
    {
        GameManager.Instance.BattleManager.Give(Value(dice));
    }
}
