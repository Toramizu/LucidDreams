using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDiceEffect : AbilityEffect
{
    protected override float AIValue { get { return 0f; } }

    public UnlockDiceEffect() { }
    public UnlockDiceEffect(int bonus, bool usesDice, float mult, bool targetsUser)
        : base(bonus, usesDice, mult, targetsUser) { }

    public override void Play(int dice)
    {
        if (targetsUser)
        {
            List<RolledDie> rolled = new List<RolledDie>(GameManager.Instance.BattleManager.RolledDice().RolledDice);

            int i = Value(dice);
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
            GameManager.Instance.BattleManager.AddTrait(GameManager.Instance.Assets.Traits["Chastity"], Value(dice), false);
        }
    }

}
