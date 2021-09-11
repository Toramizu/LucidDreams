using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] List<Ability> abilities;

    PlayerData data;
    public PlayerData OData
    {
        get { return data; }
        set { LoadPlayer(value); }
    }
    protected override CharacterData Data { get { return OData; } }

    public void LoadPlayer(PlayerData data)
    {
        this.data = data;
        LoadCharacter(data);
    }

    public override bool InflictDamage(int amount)
    {
        if (base.InflictDamage(amount))
        {
            Debug.Log("Combat lost...");
            return true;
        }
        return false;
    }

    public void SetAbility(AbilityData data, int slot)
    {
        //Debug.Log("Ability? " + slot + " <= " + slot);
        if (slot <= abilities.Count)
            abilities[slot].Init(data);
    }
}
