using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueChoices : DialogueElement
{
    [XmlAttribute("Title")]
    public string ChoiceTitle { get; set; }
    [XmlElement("Choice")]
    public List<DialogueChoice> Choices { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        dialUI.Play(ChoiceTitle, Choices);
        return false;
    }
}

[System.Serializable]
public class DialogueChoice
{
    [XmlAttribute("Text")]
    public string Text { get; set; }

    [XmlElement("If")]
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
    public List<DialogueElement> Elements { get; set; }
}
