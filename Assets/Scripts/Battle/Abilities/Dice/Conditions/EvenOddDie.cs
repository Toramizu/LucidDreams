using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EvenOddDie : DiceCondition
{
    [XmlAttribute("isEven")]
    public bool Even { get; set; }

    [XmlIgnore]
    public override int[] AcceptedValues {
        get {
            if (Even)
                return new int[] { 0, 2, 4, 6 };
            else
                return new int[] { 0, 1, 3, 5 };
        }
    }

    public EvenOddDie() { }
    public EvenOddDie(bool even) { this.Even = even; }

    public override bool Check(int die)
    {
        return base.Check(die) && (((die % 2 == 0) == Even) || die == 0);
    }

    public override string ConditionText()
    {
        if (Even)
            return "EVEN";
        else
            return "ODD";
    }
}
