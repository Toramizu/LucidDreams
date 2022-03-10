using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueCondition : DialogueElement
{
    [XmlElement("If")]
    public List<DialogueIf> Ifs { get; set; } = new List<DialogueIf>();

    public override bool Play(DialogueUI dialUI)
    {
        foreach(DialogueIf dIf in Ifs)
            if (dIf.Check)
            {
                dialUI.AddInFront(dIf.Elements);
                return true;
            }
        return true;
    }
}

public class DialogueIf
{
    [XmlElement("Check")]
    public MultCondition Condition { get; set; }
    [XmlIgnore]
    public bool Check { get { return Condition == null || Condition.Check; } }

    [XmlElement("Choices", typeof(DialogueChoices))]
    [XmlElement("Battle", typeof(DialogueBattle))]
    [XmlElement("ArousalFlat", typeof(DialogueAddArousal))]
    [XmlElement("Arousal", typeof(DialogueAddArousal2))]
    [XmlElement("Line", typeof(DialogueLine))]
    [XmlElement("Speaker", typeof(DialogueSpeaker))]
    [XmlElement("Relationship", typeof(DialogueRelationship))]
    [XmlElement("RelationUp", typeof(DialogueRelationStage))]
    [XmlElement("Condition", typeof(DialogueCondition))]
    [XmlElement("Roll", typeof(DialogueRoll))]
    [XmlElement("ArousalBonus", typeof(ArousalBonus))]
    [XmlElement("RelationBonus", typeof(RelationBonus))]
    [XmlElement("Loop", typeof(DialogueLoop))]
    [XmlElement("Flag", typeof(DialogueFlag))]
    [XmlElement("End", typeof(DialogueEnd))]
    [XmlElement("Next", typeof(DialogueNext))]
    [XmlElement("Input", typeof(DialogueInput))]
    [XmlElement("Dom", typeof(DialogueDom))]
    public List<DialogueElement> Elements { get; set; }
}