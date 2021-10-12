using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DiceCondition
{
    public LinkedValue Link { get; set; }
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
}

