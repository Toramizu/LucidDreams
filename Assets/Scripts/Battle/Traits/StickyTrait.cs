using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sticky", menuName = "Data/Trait/Sticky")]
public class StickyTrait : Trait
{
    public override void StartTurn(Character current, int stack)
    {
        List<RolledDie> dice = new List<RolledDie>(current.Dice.RolledDice);

        for(int i = 0; i < stack && dice.Count > 0; i++)
        {
            RolledDie die = dice[Random.Range(0, dice.Count)];
            if (die.Value > 1)
                die.Value--;
            else i++;
            dice.Remove(die);
        }

        current.Traits.RemoveTrait(this);
    }
}
