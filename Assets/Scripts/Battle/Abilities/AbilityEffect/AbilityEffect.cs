using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

public abstract class AbilityEffect
{
    [XmlAttribute("Bonus")]
    public int Bonus { get; set; }
    [XmlAttribute("DiceMult"), DefaultValue(0f)]
    public float DiceMultiplier { get; set; }
    [XmlAttribute("UsesCumulativeBonus")]
    public bool UsesCumulativeBonus { get; set; }

    [XmlIgnore]
    public DiceCondition Condition { get; set; }
    [XmlElement("Condition")]
    public DiceCondition _Condition {
        get
        {
            if (Condition is AnyDie)
                return null;
            else
                return Condition;
        }
        set
        {
            if (value == null)
                Condition = new AnyDie();
            else
                Condition = value;
        }
    }

    [XmlAttribute("TargetsUser")]
    public bool TargetsUser { get; set; }

    public AbilityEffect() { }
    public AbilityEffect(EffectData data)
    {
        Bonus = data.Bonus;
        if (data.UsesDice)
        {
            if (data.Multiplier == 0)
                DiceMultiplier = 1f;
            else
                DiceMultiplier = data.Multiplier;
        }
        else
            DiceMultiplier = 0f;
        //UsesDice = data.UsesDice;
        UsesCumulativeBonus = data.UsesCumulativeBonus;
        
        TargetsUser = data.TargetsUser;
        Condition = data.Condition.ToCondition();
    }

    protected int Value(int dice, Ability abi)
    {
        return Value(dice, abi.Used);
    }

    protected int Value(int dice, int cumulative)
    {
        /*if (UsesDice && DiceMultiplier == 0)
            DiceMultiplier = 1;*/

        int amount = Bonus + (int)(dice * DiceMultiplier);
        if (UsesCumulativeBonus)
            amount += cumulative;
        return amount;
    }
    
    public abstract void Play(Succubus user, Succubus other, int dice, Ability abi);

    public void CheckAndPlay(Succubus user, Succubus other, int dice, Ability abi)
    {
        if (Condition == null || Condition.Check(dice))
            Play(user, other, dice, abi);
    }

    public abstract AbilityEffect Clone();
}