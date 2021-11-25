using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FlagCondition : Condition
{
    [XmlAttribute("Flag")]
    string flag;
    [XmlAttribute("Comparator")]
    Comparator comparator;
    [XmlAttribute("Value")]
    int value;

    public override bool Check
    {
        get
        {
            int f = GameManager.Instance.Flags.GetFlag(flag);
            switch (comparator)
            {
                case Comparator.Equal:
                default:
                    return f == value;
                case Comparator.Different:
                    return f != value;
                case Comparator.Smaller:
                    return f < value;
                case Comparator.SmallerOrEqual:
                    return f <= value;
                case Comparator.Bigger:
                    return f > value;
                case Comparator.BiggerOrEqual:
                    return f >= value;
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