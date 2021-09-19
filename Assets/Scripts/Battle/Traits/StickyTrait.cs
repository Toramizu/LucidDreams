using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sticky", menuName = "Data/Trait/Sticky")]
public class StickyTrait : Trait
{
    public override void StartTurn(Character current)
    {
        int amount = current.Traits.TraitStack(this);
        List<RolledDie> dice = new List<RolledDie>(current.Dice.RolledDice);

        for(int i = 0; i < amount && dice.Count > 0; i++)
        {
            RolledDie die = dice[Random.Range(0, dice.Count)];
            die.Value--;
            dice.Remove(die);
        }

        current.Traits.RemoveTrait(this);
    }
}
