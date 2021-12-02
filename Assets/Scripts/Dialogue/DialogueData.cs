using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueData : XmlAsset
{
    [XmlAttribute("ID")]
    public string ID { get; set; }

    // !! Don't forget to change in Choices & Roll too
    [XmlElement("Choices", typeof(DialogueChoices))]
    [XmlElement("Battle", typeof(DialogueBattle))]
    [XmlElement("ArousalFlat", typeof(DialogueAddArousal))]
    [XmlElement("Arousal", typeof(DialogueAddArousal2))]
    [XmlElement("Line", typeof(DialogueLine))]
    [XmlElement("Speaker", typeof(DialogueSpeaker))]
    [XmlElement("Relationship", typeof(DialogueRelationship))]
    [XmlElement("Condition", typeof(DialogueCondition))]
    [XmlElement("Roll", typeof(DialogueRoll))]
    public List<DialogueElement> Elements { get; set; }
}