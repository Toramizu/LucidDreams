using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TraitsSheetUI : MonoBehaviour
{
    [SerializeField] TraitUI traitPrefab;

    Dictionary<Trait, TraitUI> traits = new Dictionary<Trait, TraitUI>();

    public void Clear()
    {
        foreach (TraitUI trait in traits.Values)
            Destroy(trait.gameObject);

        traits.Clear();
    }

    public void AddTrait(Trait trait, int amount)
    {
        if (traits.ContainsKey(trait))
            traits[trait].Amount = amount;
        else
        {
            TraitUI t = Instantiate(traitPrefab, transform, false);
            t.Init(trait, amount);

            traits.Add(trait, t);
        }
    }

    public void RemoveTrait(Trait trait)
    {
        Destroy(traits[trait].gameObject);
        traits.Remove(trait);
    }
}
