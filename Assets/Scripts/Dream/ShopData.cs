using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopData
{
    [SerializeField] List<AbilityData> abilities;
    public List<AbilityData> Abilities { get { return abilities; } }

    [SerializeField] int minAbilities;
    public int MinAbilities { get { return minAbilities; } }

    [SerializeField] int maxAbilities;
    public int MaxAbilities { get { return maxAbilities; } }

    [SerializeField] int incrementCost = 1;
    public int IncrementCost  { get { return incrementCost; } }

    [SerializeField] float abilityMod = 1;
    public float AbilityMod { get { return abilityMod; } }
}
