using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueRoll : DialogueElement
{
    [XmlAttribute("Dice"), DefaultValue(2)]
    public int Dice { get; set; } = 2;
    [XmlAttribute("Bonus")]
    public string FlagBonus { get; set; }
    [XmlAttribute("Malus")]
    public string FlagMalus { get; set; }

    [XmlElement("Result")]
    public List<RollValue> Rolls { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        string text = "";
        int total = 0;
        if (FlagBonus != null && GameManager.Instance.Flags.HasFlag(FlagBonus))
        {
            int bonus = GameManager.Instance.Flags.GetFlag(FlagBonus);
            total += bonus;
            text += "(" + bonus + ") + ";
        }

        if (FlagMalus != null && GameManager.Instance.Flags.HasFlag(FlagMalus))
        {
            int bonus = GameManager.Instance.Flags.GetFlag(FlagMalus);
            total -= bonus;
            text += "(" + -bonus + ") + ";
        }

        for (int i = 0; i < Dice; i++)
        {
            int r = Random.Range(1, 7);
            total += r;
            if (text != "")
                text += "+ ";
            text += "_" + r + " ";
        }

        foreach(RollValue val in Rolls)
        {
            if (total >= val.Value)
            {
                text += "= " + total + " => " + val.ShownName;
                dialUI.AddInFront(val.Elements);
                break;
            }
        }

        GameManager.Instance.Notify(text);

        return true;
    }
}

[System.Serializable]
public class RollValue
{
    [XmlAttribute("Value")]
    public int Value { get; set; }
    [XmlAttribute("Shown")]
    public string ShownName { get; set; }

    [XmlElement("Choices", typeof(DialogueChoices))]
    [XmlElement("Battle", typeof(DialogueBattle))]
    [XmlElement("ArousalFlat", typeof(DialogueAddArousal))]
    [XmlElement("Arousal", typeof(DialogueAddArousal2))]
    [XmlElement("Lines", typeof(DialogueLines))]
    [XmlElement("Line", typeof(DialogueLine))]
    [XmlElement("Speaker", typeof(DialogueSpeaker))]
    //[XmlElement("Condition", typeof(DialogueCondition))]
    [XmlElement("Roll", typeof(DialogueRoll))]
    public List<DialogueElement> Elements { get; set; }
}