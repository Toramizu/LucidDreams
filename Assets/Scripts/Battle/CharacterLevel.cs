using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterLevel
{
    [SerializeField] int dice;
    public int Dice { get { return dice; } }
    [SerializeField] int maxArousal;
    public int MaxArousal { get { return maxArousal; } }
    [SerializeField] int expToNext;
    public int ExpToNext { get { return expToNext; } }
    [SerializeField] List<Ability> abilities;
    public List<Ability> Abilities { get { return abilities; } }
}
