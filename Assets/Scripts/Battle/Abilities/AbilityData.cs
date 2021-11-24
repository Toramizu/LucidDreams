using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

public class AbilityData : XmlAsset
{
    [XmlAttribute("ID")]
    public string ID { get; set; }

    [XmlElement("Description")]
    public string Description { get; set; }

    [XmlIgnore]
    public List<DiceCondition> Slots { get { return _Slots.Conditions; } }
    [XmlElement("Slots")]
    public AbilityConditions _Slots { get; set; }

    [XmlAttribute("Uses"), DefaultValue(1)]
    public int Uses { get; set; }

    [XmlAttribute("EqualDice"), DefaultValue(false)]
    public bool EqualDice { get; set; }

    [XmlAttribute("Total"), DefaultValue(0)]
    public int Total { get; set; }

    [XmlIgnore]
    public List<AbilityEffect> Effects { get { return _Effects.Effects; } }
    [XmlElement("Effects")]
    public AbilityEffects _Effects { get; set; }

    [XmlAttribute("Price")]
    public int Price { get; set; }

    [XmlAttribute("Shop"), DefaultValue(false)]
    public bool ShopAbility { get; set; }
}

public class AbilityConditions
{
    [XmlElement("Any", typeof(AnyDie))]
    [XmlElement("Equals", typeof(EqualsDie))]
    [XmlElement("MinMax", typeof(MinMaxDie))]
    [XmlElement("EvenOdd", typeof(EvenOddDie))]
    public List<DiceCondition> Conditions { get; set; }

    public AbilityConditions() { }
    public AbilityConditions(List<ConditionData> conds)
    {
        Conditions = new List<DiceCondition>();
        foreach (ConditionData c in conds)
            Conditions.Add(c.ToCondition());
    }
}

public class AbilityEffects
{
    [XmlElement("Roll", typeof(RollDiceEffect))]
    [XmlElement("Trait", typeof(TraitEffect))]
    [XmlElement("UnlockDice", typeof(UnlockDiceEffect))]
    [XmlElement("Damage", typeof(DamageEffect))]
    [XmlElement("Give", typeof(GiveDiceEffect))]
    public List<AbilityEffect> Effects { get; set; }

    public AbilityEffects() { }
    public AbilityEffects(List<EffectData> effects)
    {
        Effects = new List<AbilityEffect>();
        foreach (EffectData c in effects)
            Effects.Add(c.ToEffect());
    }
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

[System.Serializable]
public class EffectData
{
    [SerializeField] EffectsEnum effect;
    public EffectsEnum Effect { get { return effect; } }

    [SerializeField] int bonus;
    public int Bonus { get { return bonus; } }
    [SerializeField] bool usesDice = true;
    public bool UsesDice { get { return usesDice; } }
    [SerializeField] bool usesCumulativeBonus = true;
    public bool UsesCumulativeBonus { get { return usesCumulativeBonus; } }
    [SerializeField] float multiplier = 1f;
    public float Multiplier { get { return multiplier; } }
    [SerializeField] string stringValue;
    public string StringValue { get { return stringValue; } }

    [SerializeField] bool targetsUser;
    public bool TargetsUser { get { return targetsUser; } }
    [SerializeField] ConditionData condition;
    public ConditionData Condition { get { return condition; } }

    public AbilityEffect ToEffect()
    {
        switch (effect)
        {
            case EffectsEnum.Trait:
                return new TraitEffect(this);
                
            case EffectsEnum.Roll:
                return new RollDiceEffect(this);

            case EffectsEnum.Give:
                return new GiveDiceEffect(this);


            case EffectsEnum.Unlock:
                return new UnlockDiceEffect(this);
            
            case EffectsEnum.Damage:
            default:
                return new DamageEffect(this);
        }
    }
}

public enum EffectsEnum
{
    Damage,
    Trait,
    Roll,
    Give,
    Unlock,
    Deny,
    Cumul,
}
