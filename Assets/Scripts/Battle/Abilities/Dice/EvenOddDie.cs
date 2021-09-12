using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvenOddDie : DiceCondition
{
    [SerializeField] bool odd;
    public bool Even { get { return odd; } set { odd = value; } }

    public EvenOddDie() { }
    public EvenOddDie(bool odd) { this.odd = odd; }

    public override bool Check(int die)
    {
        return ((die % 2 == 0) != odd) || die == 0;
    }

    public override string ConditionText()
    {
        if (odd)
            return "ODD";
        else
            return "EVEN";
    }
}
