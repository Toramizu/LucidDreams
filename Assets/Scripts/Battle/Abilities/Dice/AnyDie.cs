using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyDie : DiceCondition
{
    public override bool Check(int die)
    {
        return true;
    }

    public override string ConditionText()
    {
        return "";
    }
}