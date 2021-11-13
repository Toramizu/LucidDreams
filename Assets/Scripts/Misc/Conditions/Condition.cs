using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Condition
{
    [SerializeField] string flag;
    [SerializeField] int value;
    [SerializeField] string sValue;
    [SerializeField] bool compareString;
    [SerializeField] Comparator comparator;

    public virtual bool Check
    {
        get
        {
            if (compareString)
            {
                string s = GameManager.Instance.Flags.GetString(flag);
                switch (comparator)
                {
                    case Comparator.Equal:
                    default:
                        return s == sValue;
                    case Comparator.Different:
                        return s != sValue;
                }
            }
            else
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
}

public enum Comparator
{
    Equal,
    Different,
    Smaller,
    SmallerOrEqual,
    BiggerOrEqual,
    Bigger
}