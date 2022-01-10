using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueData : XmlAsset
{
    [XmlAttribute("ID")]
    public string ID { get; set; }

    // !! Don't forget to change elsewhere too
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
    public List<DialogueElement> Elements { get; set; }
}