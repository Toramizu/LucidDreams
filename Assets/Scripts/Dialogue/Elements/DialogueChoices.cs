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
