using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chastity", menuName = "Data/Trait/Chastity")]
public class ChastityTrait : Trait
{
    public override void StartTurn(Succubus current, int stack)
    {
        List<RolledDie> dice = new List<RolledDie>(current.Dice.RolledDice);

        for (int i = 0; i < stack && dice.Count > 0; i++)
        {
            RolledDie die = dice[Random.Range(0, dice.Count)];
            die.Locked = true;
            dice.Remove(die);
        }

        current.Traits.RemoveTrait(this);
    }
}
