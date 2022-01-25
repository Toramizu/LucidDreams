using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Parser : MonoBehaviour
{
    [SerializeField] List<ParserItem> symbols;
    [SerializeField] string abilityBonus = "_++";

    Dictionary<string, string> strings;
    Dictionary<string, string> flags;

    private void Start()
    {
        strings = new Dictionary<string, string>();
        foreach (ParserItem i in symbols)
            strings.Add(i.Replaced, i.Replacer);

        flags = Flags.Instance.Strings;
    }

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

    public string Parse(string text)
    {
        /*foreach (ParserItem symbol in symbols)
            text = text.Replace(symbol.Replaced, symbol.Replacer);

        return text;*/
        if (text == null)
            return "";

        string words = string.Join("|", flags.Keys);
        text = Regex.Replace(text, $@"\b({words})\b", delegate (Match m)
        {
            return flags[m.Value];
        });
        words = string.Join("|", strings.Keys);
        text = Regex.Replace(text, $@"\b({words})\b", delegate (Match m)
        {
            return strings[m.Value];
        });
        return text;
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
