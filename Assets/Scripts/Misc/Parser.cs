using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser : MonoBehaviour
{
    [SerializeField] List<ParserItem> symbols;
    [SerializeField] string abilityBonus = "_++";

    public string ParseDescription(string description)
    {
        foreach (ParserItem symbol in symbols)
            description = description.Replace(symbol.Replaced, symbol.Replacer);

        return description;
        //return description.Replace(dieReplacedSymbol, dieSymbol);
    }

    public string ParseAbilityDescription(Ability ability, Character chara)
    {
        string descr = ParseDescription(ability.Data.Description);

        if (descr.Contains(abilityBonus))
            descr = descr.Replace(abilityBonus, ability.Used.ToString());

        return descr;
    }
}

[System.Serializable]
public class ParserItem
{
    [SerializeField] string replaced;
    public string Replaced { get { return replaced; } }
    [SerializeField] string replacer;
    public string Replacer { get { return replacer; } }
}
