using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCondition : Condition
{
    [SerializeField] string flag;
    [SerializeField] int value;
    [SerializeField] Comparator comparator;

    public override bool Check
    {
        get
        {
            int f = GameManager.Instance.Flags.GetFlag(flag);
            switch (comparator)
            {
                case Comparator.Equal:
                default:
                    return f == value;
                case Comparator.Different:
                    return f != value;
                case Comparator.Smaller:
                    return f < value;
                case Comparator.SmallerOrEqual:
                    return f <= value;
                case Comparator.Bigger:
                    return f > value;
                case Comparator.BiggerOrEqual:
                    return f >= value;
            }
        }
    }
}