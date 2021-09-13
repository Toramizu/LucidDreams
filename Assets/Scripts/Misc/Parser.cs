using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser : MonoBehaviour
{
    [SerializeField] string dieSymbol = "▢";
    [SerializeField] string dieReplacedSymbol = "_";

    public string ParseDescription(string description)
    {
        return description.Replace(dieReplacedSymbol, dieSymbol);
    }
}
