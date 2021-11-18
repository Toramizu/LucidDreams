using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TraitsSheet
{
    public TraitsSheetUI TraitsUI { get; set; }

    public Dictionary<Trait, int> Traits { get; private set; } = new Dictionary<Trait, int>();

    public bool HasTrait(Trait trait) {
        return Traits.ContainsKey(trait);
    }
    public int TraitStack(Trait trait)
    {
        if (Traits.ContainsKey(trait))
            return Traits[trait];
        else
            return 0;
    }

    public void AddTrait(Trait trait, int amount)
    {
        if (Traits.ContainsKey(trait))
            Traits[trait] += amount;
        else
            Traits.Add(trait, amount);

        if (trait.ReversrTrait != null && Traits[trait] < 0)
            AddTrait(trait.ReversrTrait, -Traits[trait]);

        if(Traits[trait] <= 0)
        {
            Traits.Remove(trait);
            if (TraitsUI != null)
                TraitsUI.RemoveTrait(trait);
        } else if (TraitsUI != null)
            TraitsUI.AddTrait(trait, Traits[trait]);
    }

    public void RemoveTrait(Trait trait)
    {
        Traits.Remove(trait);

        if (TraitsUI != null)
            TraitsUI.RemoveTrait(trait);
    }

    public void Clear()
    {
        Traits.Clear();

        if (TraitsUI != null)
            TraitsUI.Clear();
    }

    public TraitsSheet Clone()
    {
        TraitsSheet t = new TraitsSheet();
        t.Traits = new Dictionary<Trait, int>(Traits);
        return t;
    }

    public float AIValue
    {
        get
        {
            float val = 0;

            foreach (Trait t in Traits.Keys)
                val += t.AIValue * Traits[t];

            return val;
        }
    }

    #region Events

    public void OnAttack(ref int damages, Succubus current, Succubus other)
    {
        List<Trait> t = new List<Trait>(Traits.Keys);
        foreach (Trait trait in t)
            trait.OnAttack(ref damages, current, other, Traits[trait]);
    }
    public void OnDefense(ref int damages, Succubus current, Succubus other)
    {
        List<Trait> t = new List<Trait>(Traits.Keys);
        foreach (Trait trait in t)
            trait.OnDefense(ref damages, current, other, Traits[trait]);
    }

    public void StartTurn(Succubus current)
    {
        List<Trait> t = new List<Trait>(Traits.Keys);
        foreach (Trait trait in t)
            trait.StartTurn(current, Traits[trait]);
    }
    public void EndTurn(Succubus current)
    {
        List<Trait> t = new List<Trait>(Traits.Keys);
        foreach (Trait trait in t)
            trait.EndTurn(current, Traits[trait]);
    }
    #endregion
}
