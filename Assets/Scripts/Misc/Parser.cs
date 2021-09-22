using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser : MonoBehaviour
{
    [SerializeField] string dieSymbol = "▢";
    [SerializeField] string dieReplacedSymbol = "_";
    [SerializeField] List<ParserItem> symbols;

    public string ParseDescription(string description)
    {
        foreach (ParserItem symbol in symbols)
            description = description.Replace(symbol.Replaced, symbol.Replacer);

        return description;
        //return description.Replace(dieReplacedSymbol, dieSymbol);
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
