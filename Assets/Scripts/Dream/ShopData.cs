using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
public class ShopData
{
    [XmlAttribute("MinAbis"), DefaultValue(1)]
    public int MinAbilities { get; set; }

    [XmlAttribute("MaxAbis"), DefaultValue(1)]
    public int MaxAbilities { get; set; }

    [XmlAttribute("IncrementCost"), DefaultValue(5)]
    public int IncrementCost { get; set; }

    [XmlAttribute("AbilityCostMod"), DefaultValue(1f)]
    public float AbilityMod { get; set; }
}
