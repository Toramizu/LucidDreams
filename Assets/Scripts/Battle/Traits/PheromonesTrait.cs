using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pheromones", menuName = "Data/Trait/Pheromones")]
public class PheromonesTrait : Trait
{
    public override void OnDefense(ref int damages, Character current, Character other, int stack)
    {
        GameManager.Instance.BattleManager.InflictsDamage(stack, true, true);
    }

    public override void StartTurn(Character current, int stack)
    {
        current.Traits.RemoveTrait(this);
    }
}
