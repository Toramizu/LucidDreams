using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitEffect : AbilityEffect
{
    Trait trait;

    protected override float AIValue { get { return trait.AIValue; } }

    public TraitEffect() { }
    /*public TraitEffect(string trait, int bonus, bool usesDice, bool usesCumulativeBonus, float mult, bool targetsUser, DiceCondition condition)
        : base(bonus, usesDice, usesCumulativeBonus, mult, targetsUser, condition)
    {
        this.trait = GameManager.Instance.Assets.Traits[trait];
    }*/
    public TraitEffect(EffectData data) : base(data)
    {
        this.trait = GameManager.Instance.Assets.Traits[data.StringValue];
    }

    public override void Play(int dice, Ability abi)
    {
        GameManager.Instance.BattleManager.AddTrait(trait, Value(dice, abi), targetsUser);
    }

    public override void GetAIValue(int dice, AIData current, Ability abi)
    {
        if (condition != null && !condition.Check(dice))
            return;

        int v = Value(dice, abi);
        if (targetsUser)
            current.User.ApplyTrait(trait, v);
        else
            current.Target.ApplyTrait(trait, v);

        /*SimpleCharacter target;
        if (targetsUser)
            target = current.User;
        else
            target = current.Target;

        
        int v = Value(dice, 0);
        int applied = target.Traits[trait];
        if (v > trait.MaxStack - applied)
            v = trait.MaxStack - applied;


        //return AIValue * v;*/
    }
}
