using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TraitsSheet
{
    [SerializeField] Transform traitHolder;
    [SerializeField] TraitUI traitPrefab;
    
    Dictionary<Trait, TraitUI> traits = new Dictionary<Trait, TraitUI>();

    public bool HasTrait(Trait trait) {
        return traits.ContainsKey(trait);
    }
    public int TraitStack(Trait trait)
    {
        if (traits.ContainsKey(trait))
            return traits[trait].Amount;
        else
            return 0;
    }

    public Dictionary<Trait, int> ToSimpleDictionary
    {
        get
        {
            Dictionary<Trait, int> dict = new Dictionary<Trait, int>();
            foreach (Trait t in traits.Keys)
                dict.Add(t, traits[t].Amount);
            return dict;
        }
    }

    public void AddTrait(Trait trait, int amount)
    {
        if (traits.ContainsKey(trait))
            traits[trait].Amount += amount;
        else
            AddNewTrait(trait, amount);

        if (traits[trait].Amount <= 0)
        {
            if (traits[trait].Amount < 0 && trait.ReversrTrait != null)
                AddTrait(trait.ReversrTrait, -traits[trait].Amount);

            RemoveTrait(trait);
        } else if (trait.MaxStack > 0 && traits[trait].Amount > trait.MaxStack)
        {
            traits[trait].Amount = trait.MaxStack;
        }
    }

    public void RemoveTrait(Trait trait)
    {
        MonoBehaviour.Destroy(traits[trait].gameObject);
        traits.Remove(trait);
    }

    void AddNewTrait(Trait trait, int amount)
    {
        TraitUI t = MonoBehaviour.Instantiate(traitPrefab, traitHolder, false);
        t.Init(trait, amount);

        traits.Add(trait, t);
    }

    public void Clear()
    {
        foreach (TraitUI t in traits.Values)
            MonoBehaviour.Destroy(t.gameObject);

        traits.Clear();
    }

    #region Events

    public void OnAttack(ref int damages, Character current, Character other)
    {
        List<Trait> t = new List<Trait>(traits.Keys);
        foreach (Trait trait in t)
            trait.OnAttack(ref damages, current, other, traits[trait].Amount);
    }
    public void OnDefense(ref int damages, Character current, Character other)
    {
        List<Trait> t = new List<Trait>(traits.Keys);
        foreach (Trait trait in t)
            trait.OnDefense(ref damages, current, other, traits[trait].Amount);
    }

    public void StartTurn(Character current)
    {
        List<Trait> t = new List<Trait>(traits.Keys);
        foreach (Trait trait in t)
            trait.StartTurn(current, traits[trait].Amount);
    }
    public void EndTurn(Character current)
    {
        List<Trait> t = new List<Trait>(traits.Keys);
        foreach (Trait trait in t)
            trait.EndTurn(current, traits[trait].Amount);
    }
    #endregion
}
