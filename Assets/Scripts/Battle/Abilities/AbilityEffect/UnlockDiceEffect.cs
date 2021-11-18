using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDiceEffect : AbilityEffect
{
    public UnlockDiceEffect() { }
    public UnlockDiceEffect(EffectData data) : base(data)
    { }
    
    public override void Play(Succubus user, Succubus other, int dice, Ability abi)
    {
        if (targetsUser)
        {
            List<RolledDie> rolled = new List<RolledDie>(user.Dice.RolledDice);

            int i = Value(dice, abi);
            while (i > 0 && rolled.Count > 0)
            {
                RolledDie die = rolled[Random.Range(0, rolled.Count)];
                rolled.Remove(die);

                if (die.Locked)
                {
                    die.Locked = false;
                    i--;
                }
            }

            if (i > 0)
                user.Traits.AddTrait(GameManager.Instance.Assets.Traits["Chastity"], -i);
        }
        else
        {
            other.Traits.AddTrait(GameManager.Instance.Assets.Traits["Chastity"], -Value(dice, abi));
        }
    }

    public override AbilityEffect Clone()
    {
        return new UnlockDiceEffect();
    }
}
