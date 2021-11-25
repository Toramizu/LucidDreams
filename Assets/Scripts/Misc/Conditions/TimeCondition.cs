using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCondition : Condition
{
    public int Time { get; set; }

    public override bool Check
    {
        get
        {
            return GameManager.Instance.DayManager.Time == Time;
        }
    }
}
