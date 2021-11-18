using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser : MonoBehaviour
{
    [SerializeField] List<ParserItem> symbols;
    [SerializeField] string abilityBonus = "_++";

    public string ParseDescription(string description, Dictionary<string, string> strings)
    {
        foreach (ParserItem symbol in symbols)
            description = description.Replace(symbol.Replaced, symbol.Replacer);

        foreach(string id in strings.Keys)
            description = description.Replace(id, strings[id]);

        return description;
        //return description.Replace(dieReplacedSymbol, dieSymbol);
    }

    public string ParseAbilityDescription(Ability ability, Succubus chara, Dictionary<string, string> strings)
    {
        string descr = ParseDescription(ability.Data.Description, strings);

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
