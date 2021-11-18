using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Aroused", menuName = "Data/Trait/Aroused")]
public class ArousedTrait : Trait
{
    public override void EndTurn(Succubus current, int stack)
    {
        //GameManager.Instance.BattleManager.InflictsDamage(stack, true, true);
        current.InflictDamage(stack);
        current.Traits.AddTrait(this, -1);
    }
}
