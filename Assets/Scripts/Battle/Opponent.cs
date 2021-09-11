using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Opponent : Character
{
    OpponentData data;
    public OpponentData OData
    {
        get { return data; }
        set { LoadOpponent(value); }
    }
    protected override CharacterData Data { get { return OData; } }

    public void LoadOpponent(OpponentData data)
    {
        this.data = data;
        LoadCharacter(data);
    }

    public override bool InflictDamage(int amount)
    {
        if (base.InflictDamage(amount))
        {
            Debug.Log("Combat won!");
            return true;
        }
        return false;
    }
}
