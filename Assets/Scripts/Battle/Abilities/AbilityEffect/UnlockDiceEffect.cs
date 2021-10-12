using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDiceEffect : AbilityEffect
{
    public UnlockDiceEffect() { }
    /*public UnlockDiceEffect(int bonus, bool usesDice, bool usesCumulativeBonus, float mult, bool targetsUser, DiceCondition condition)
        : base(bonus, usesDice, usesCumulativeBonus, mult, targetsUser, condition) { }*/
    public UnlockDiceEffect(EffectData data) : base(data)
    { }

    public override void Play(int dice, Ability abi)
    {
        if (targetsUser)
        {
            List<RolledDie> rolled = new List<RolledDie>(GameManager.Instance.BattleManager.RolledDice().RolledDice);

            int i = Value(dice, abi);
            while(i > 0 && rolled.Count > 0)
            {
                RolledDie die = rolled[Random.Range(0, rolled.Count)];
                rolled.Remove(die);

                if (die.Locked)
                {
                    die.Locked = false;
                    i--;
                }
            }

            if(i > 0)
                GameManager.Instance.BattleManager.AddTrait(GameManager.Instance.Assets.Traits["Chastity"], i, true);
        }
        else
        {
            GameManager.Instance.BattleManager.AddTrait(GameManager.Instance.Assets.Traits["Chastity"], Value(dice, abi), false);
        }
    }

}
