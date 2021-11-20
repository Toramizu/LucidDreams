using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueLine : DialogueElement
{
    [XmlText]
    public string Line { get; set; }
    [XmlIgnore]
    public ImageHaver Speaker { get; set; }
    [XmlIgnore]
    public SpeakerPos SpeakerPosition { get; set; }
    [XmlIgnore]
    public bool RemoveSpeaker { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        dialUI.PlayLine(this);
        return false;
    }
}