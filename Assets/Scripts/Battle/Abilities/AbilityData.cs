using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Data/Ability", order = 2)]
public class AbilityData : ScriptableObject
{
    [SerializeField] string title;
    public string Title { get { return title; } }

    [SerializeField] string description;
    public string Description { get { return description; } }

    [SerializeField] List<ConditionData> conditions;
    public List<ConditionData> Conditions { get { return conditions; } }

    [SerializeField] int uses;
    public int Uses { get { return uses; } }

    [SerializeField] bool equalDice;
    public bool EqualDice { get { return equalDice; } }

    [SerializeField] int total;
    public int Total { get { return total; } }

}

[System.Serializable]
public class ConditionData
{
    [SerializeField] ConditionType type;
    public ConditionType Type { get { return type; } }

    [SerializeField] int value;
    public int Value { get { return value; } }

    [SerializeField] bool toggle;
    public bool Toggle { get { return toggle; } }

    public DiceCondition ToCondition()
    {
        switch (type)
        {
            case ConditionType.Equals:
                return new EqualsDie(value);
            case ConditionType.EvenOdd:
                return new EvenOddDie(toggle);
            case ConditionType.MinMax:
                return new MinMaxDie(toggle, value);

            case ConditionType.Any:
            default:
                return new AnyDie();
        }
    }
}

public enum ConditionType
{
    Any,
    Equals,
    EvenOdd,
    MinMax,
}