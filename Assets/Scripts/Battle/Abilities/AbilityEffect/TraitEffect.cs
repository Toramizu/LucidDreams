using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitEffect : AbilityEffect
{
    Trait trait;

    public TraitEffect() { }
    public TraitEffect(string trait, int bonus, bool usesDice, float mult, bool targetsUser)
        : base(bonus, usesDice, mult, targetsUser)
    {
        this.trait = GameManager.Instance.Assets.Traits[trait];
    }

    public override void Play(int dice)
    {
        int amount = bonus;
        if (usesDice)
            amount += (int)(dice * multiplier);

        GameManager.Instance.BattleManager.AddTrait(trait, amount, targetsUser);
    }

}
