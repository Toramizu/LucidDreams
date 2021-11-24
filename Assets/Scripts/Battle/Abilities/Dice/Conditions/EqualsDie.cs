using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EqualsDie : DiceCondition
{
    [XmlAttribute("Value")]
    public int Value { get; set; }

    [XmlIgnore]
    public override int[] AcceptedValues { get { return new int[] { 0, Value }; } }

    public EqualsDie() { }
    public EqualsDie(int value) { this.Value = value; }

    public override bool Check(int die)
    {
        return base.Check(die) && (die == Value || die == 0);
    }

    public override string ConditionText()
    {
        return Value.ToString();
    }
}
