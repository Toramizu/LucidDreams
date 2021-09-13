using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] List<Ability> abilities;
    public int Rolls { get; set; }

    PlayerData data;
    public PlayerData OData
    {
        get { return data; }
        set { LoadPlayer(value); }
    }
    protected override CharacterData Data { get { return OData; } }

    public bool NoAbilityRemaining
    {
        get
        {
            foreach (Ability abi in abilities)
                if (abi.isActiveAndEnabled)
                    return false;

            return true;
        }
    }

    public void LoadPlayer(PlayerData data)
    {
        this.data = data;
        LoadCharacter(data);

        Rolls = data.Rolls;
        for(int i = 0; i < abilities.Count; i++)
        {
            if (i < data.Abilities.Count)
                SetAbility(data.Abilities[i], i);
            else
                abilities[i].gameObject.SetActive(false);
        }
    }
    
    public void SetAbility(AbilityData data, int slot)
    {
        //Debug.Log("Ability? " + slot + " <= " + slot);
        if (slot <= abilities.Count)
        {
            abilities[slot].gameObject.SetActive(true);
            abilities[slot].Init(data);
        }
    }

    public void NewRound()
    {
        foreach (Ability abi in abilities)
            if (abi.isActiveAndEnabled)
                abi.ResetAbility();
    }
}
