using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public abstract class DiceCondition
{
    [XmlIgnore]
    public LinkedValue Link { get; set; }
    [XmlIgnore]
    public abstract int[] AcceptedValues { get; }

    public virtual bool Check(int die)
    {
        return Link == null || Link.Value == die || Link.Value == 0;
    }

    public abstract string ConditionText();
}

public class LinkedValue
{
    public int Value { get; set; }
    public int Count { get; set; }

    public LinkedValue() { }
    public LinkedValue(LinkedValue l) {
        Value = l.Value;
        Count = l.Count;
    }
}

