using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxDie : DiceCondition
{
    [SerializeField] int value;
    public int Value { get { return value; } set { this.value = value; } }

    [SerializeField] bool max;
    public bool Max { get { return max; } set { max = value; } }

    public override int[] AcceptedValues {
        get {
            List<int> vals = new List<int>() { 0 };

            if (max)
                for (int i = 1; i <= value; i++)
                    vals.Add(i);
            else
                for (int i = value; i <= 6; i++)
                    vals.Add(i);

            return vals.ToArray();
        }
    }

    public MinMaxDie() { }
    public MinMaxDie(bool max, int value) { this.max = max; this.value = value; }

    public override bool Check(int die)
    {
        return base.Check(die) && ((die == value || (die < value) == max) || die == 0);
    }

    public override string ConditionText()
    {
        if (max)
            return "max\n" + value;
        else
            return "min\n" + value;
    }
}
