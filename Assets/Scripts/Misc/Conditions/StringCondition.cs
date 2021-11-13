using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringCondition : Condition
{
    [SerializeField] public string flag;
    [SerializeField] public string value;
    [SerializeField] public bool equal;

    public override bool Check
    {
        get
        {
            return (GameManager.Instance.Flags.GetString(flag) == value) == equal;
        }
    }
}
