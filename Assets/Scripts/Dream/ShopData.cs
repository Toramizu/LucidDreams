using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopData
{
    [SerializeField] List<AbilityData> abilities;
    public List<AbilityData> Abilities { get { return abilities; } }

    [SerializeField] int maxAbilities;
    public int MaxAbilities { get { return maxAbilities; } }

    [SerializeField] int arousalMod = 1;
    public int ArousalMod { get { return arousalMod; } }

    [SerializeField] int abilityMod = 1;
    public int AbilityMod { get { return abilityMod; } }
}
