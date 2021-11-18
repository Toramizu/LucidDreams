using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pheromones", menuName = "Data/Trait/Pheromones")]
public class PheromonesTrait : Trait
{
    public override void OnDefense(ref int damages, Succubus current, Succubus other, int stack)
    {
        //GameManager.Instance.BattleManager.InflictsDamage(stack, true, true);
        other.InflictDamage(stack);
    }

    public override void StartTurn(Succubus current, int stack)
    {
        current.Traits.RemoveTrait(this);
    }
}
