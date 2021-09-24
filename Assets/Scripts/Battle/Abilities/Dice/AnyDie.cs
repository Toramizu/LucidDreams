using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyDie : DiceCondition
{
    public override int[] AcceptedValues { get { return new int[] { 0, 1, 2, 3, 4, 5, 6 }; } }

    public override bool Check(int die)
    {
        return base.Check(die);
    }

    public override string ConditionText()
    {
        return "";
    }
}
