using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Aroused", menuName = "Data/Trait/Aroused")]
public class ArousedTrait : Trait
{
    public override void EndTurn(Character current)
    {
        GameManager.Instance.BattleManager.InflictsDamage(current.Traits.TraitStack(this), true, true);
        current.Traits.AddTrait(this, -1);
    }
}
