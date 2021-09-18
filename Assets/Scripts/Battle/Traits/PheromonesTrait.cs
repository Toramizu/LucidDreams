using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pheromones", menuName = "Data/Trait/Pheromones")]
public class PheromonesTrait : Trait
{
    public override void OnDefense(ref int damages, Character current, Character other)
    {
        GameManager.Instance.BattleManager.InflictsDamage(current.Traits.TraitStack(this), true, true);
    }

    public override void StartTurn(Character current)
    {
        current.Traits.RemoveTrait(this);
    }
}
