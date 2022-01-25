using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FlagCondition : Condition
{
    [XmlAttribute("Flag")]
    public string Flag { get; set; }
    [XmlAttribute("Comparator")]
    public Comparator Comparator { get; set; }
    [XmlAttribute("Value")]
    public int Value { get; set; }

    public override bool Check
    {
        get
        {
            int f = Flags.Instance.GetFlag(Flag);
            switch (Comparator)
            {
                case Comparator.Equal:
                default:
                    return f == Value;
                case Comparator.Different:
                    return f != Value;
                case Comparator.Smaller:
                    return f < Value;
                case Comparator.SmallerOrEqual:
                    return f <= Value;
                case Comparator.Bigger:
                    return f > Value;
                case Comparator.BiggerOrEqual:
                    return f >= Value;
            }
        }
    }
}

public enum Comparator
{
    Equal,
    Different,
    Smaller,
    SmallerOrEqual,
    BiggerOrEqual,
    Bigger
}