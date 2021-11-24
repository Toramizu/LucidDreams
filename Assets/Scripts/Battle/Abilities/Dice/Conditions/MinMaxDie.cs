using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class MinMaxDie : DiceCondition
{
    [XmlAttribute("Value")]
    public int Value { get; set; }

    [XmlAttribute("IsMax")]
    public bool Max { get; set; }

    [XmlIgnore]
    public override int[] AcceptedValues {
        get {
            List<int> vals = new List<int>() { 0 };

            if (Max)
                for (int i = 1; i <= Value; i++)
                    vals.Add(i);
            else
                for (int i = Value; i <= 6; i++)
                    vals.Add(i);

            return vals.ToArray();
        }
    }

    public MinMaxDie() { }
    public MinMaxDie(bool max, int value) { this.Max = max; this.Value = value; }

    public override bool Check(int die)
    {
        return base.Check(die) && ((die == Value || (die < Value) == Max) || die == 0);
    }

    public override string ConditionText()
    {
        if (Max)
            return "max\n" + Value;
        else
            return "min\n" + Value;
    }
}
