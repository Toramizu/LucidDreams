using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitEffect : AbilityEffect
{
    Trait trait;

    protected override float AIValue { get { return trait.AIValue; } }

    public TraitEffect() { }
    public TraitEffect(string trait, int bonus, bool usesDice, float mult, bool targetsUser, DiceCondition condition)
        : base(bonus, usesDice, mult, targetsUser, condition)
    {
        this.trait = GameManager.Instance.Assets.Traits[trait];
    }

    public override void Play(int dice)
    {
        GameManager.Instance.BattleManager.AddTrait(trait, Value(dice), targetsUser);
    }

    public override float GetAIValue(int dice, AIData current)
    {
        if (condition != null && !condition.Check(dice))
            return 0f;

        Character target;
        if (targetsUser)
            target = current.User;
        else
            target = current.Target;


        int v = Value(dice);
        int applied = target.Traits.TraitStack(trait);
        if (v > trait.MaxStack - applied)
            v = trait.MaxStack - applied;

        return AIValue * v;
    }
}
