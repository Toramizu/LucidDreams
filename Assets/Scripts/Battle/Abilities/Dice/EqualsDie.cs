using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqualsDie : DiceCondition
{
    [SerializeField] int value;
    public int Value { get { return value; } set { this.value = value; } }

    public EqualsDie() { }
    public EqualsDie(int value) { this.value = value; }

    public override bool Check(int die)
    {
        return die == value || die == 0;
    }

    public override string ConditionText()
    {
        return value.ToString();
    }
}
