using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TraitsSheet
{
    public TraitsSheetUI TraitsUI { get; set; }

    /*[SerializeField] Transform traitHolder;
    [SerializeField] TraitUI traitPrefab;
    
    Dictionary<Trait, TraitUI> traits = new Dictionary<Trait, TraitUI>();*/
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

    /*public Dictionary<Trait, int> Traits
    {
        get
        {
            Dictionary<Trait, int> dict = new Dictionary<Trait, int>();
            foreach (Trait t in Traits.Keys)
                dict.Add(t, Traits[t].Amount);
            return dict;
        }
    }*/

    public void AddTrait(Trait trait, int amount)
    {
        if (Traits.ContainsKey(trait))
            Traits[trait] += amount;
        else
            Traits.Add(trait, amount);

        if (TraitsUI != null)
            TraitsUI.AddTrait(trait, amount);

        /*if (Traits.ContainsKey(trait))
            Traits[trait].Amount += amount;
        else
            AddNewTrait(trait, amount);

        if (Traits[trait].Amount <= 0)
        {
            if (Traits[trait].Amount < 0 && trait.ReversrTrait != null)
                AddTrait(trait.ReversrTrait, -Traits[trait].Amount);

            RemoveTrait(trait);
        } else if (trait.MaxStack > 0 && Traits[trait].Amount > trait.MaxStack)
        {
            Traits[trait].Amount = trait.MaxStack;
        }*/
    }

    public void RemoveTrait(Trait trait)
    {
        //MonoBehaviour.Destroy(Traits[trait].gameObject);
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

    /*void AddNewTrait(Trait trait, int amount)
    {
        TraitUI t = MonoBehaviour.Instantiate(traitPrefab, traitHolder, false);
        t.Init(trait, amount);

        Traits.Add(trait, t);
    }

    public void Clear()
    {
        foreach (TraitUI t in Traits.Values)
            MonoBehaviour.Destroy(t.gameObject);

        Traits.Clear();
    }*/

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

    public void OnAttack(ref int damages, Character current, Character other)
    {
        List<Trait> t = new List<Trait>(Traits.Keys);
        foreach (Trait trait in t)
            trait.OnAttack(ref damages, current, other, Traits[trait]);
    }
    public void OnDefense(ref int damages, Character current, Character other)
    {
        List<Trait> t = new List<Trait>(Traits.Keys);
        foreach (Trait trait in t)
            trait.OnDefense(ref damages, current, other, Traits[trait]);
    }

    public void StartTurn(Character current)
    {
        List<Trait> t = new List<Trait>(Traits.Keys);
        foreach (Trait trait in t)
            trait.StartTurn(current, Traits[trait]);
    }
    public void EndTurn(Character current)
    {
        List<Trait> t = new List<Trait>(Traits.Keys);
        foreach (Trait trait in t)
            trait.EndTurn(current, Traits[trait]);
    }
    #endregion
}
