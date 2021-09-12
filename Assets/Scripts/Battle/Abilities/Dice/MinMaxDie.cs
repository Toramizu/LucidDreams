using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxDie : DiceCondition
{
    [SerializeField] int value;
    public int Value { get { return value; } set { this.value = value; } }

    [SerializeField] bool max;
    public bool Max { get { return max; } set { max = value; } }

    public MinMaxDie() { }
    public MinMaxDie(bool max, int value) { this.max = max; this.value = value; }

    public override bool Check(int die)
    {
        return (die == value || (die < value) == max) || die == 0;
    }

    public override string ConditionText()
    {
        if (max)
            return "max\n" + value;
        else
            return "min\n" + value;
    }
}
