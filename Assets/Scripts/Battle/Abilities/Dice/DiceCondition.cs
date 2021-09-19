using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DiceCondition
{
    public LinkedValue Linked { get; set; }

    public virtual bool Check(int die)
    {
        return Linked == null || Linked.Value == die || Linked.Value == 0;
    }

    public abstract string ConditionText();
}

public class LinkedValue
{
    public int Value { get; set; }
    public int Count { get; set; }
}

