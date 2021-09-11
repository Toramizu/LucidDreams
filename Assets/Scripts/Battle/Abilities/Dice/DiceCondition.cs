using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DiceCondition
{
    public abstract bool Check(int die);

    public abstract string ConditionText();
}
