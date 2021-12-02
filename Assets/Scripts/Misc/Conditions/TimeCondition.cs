using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class TimeCondition : Condition
{
    [XmlAttribute("Comparator")]
    Comparator comparator;
    [XmlAttribute("Value")]
    public int Value { get; set; }

    public override bool Check
    {
        get
        {
            int f = GameManager.Instance.DayManager.Time;
            switch (comparator)
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
