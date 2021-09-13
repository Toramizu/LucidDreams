using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Data/Player", order = 4)]
public class PlayerData : CharacterData
{
    [SerializeField] List<AbilityData> abilities;
    public List<AbilityData> Abilities { get { return abilities; } set { abilities = value; } }

    [SerializeField] int rolls = 3;
    public int Rolls { get { return rolls; } set { rolls = value; } }
}
