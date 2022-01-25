using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueLine : DialogueElement
{
    [XmlText]
    public string Line { get; set; }
    [XmlAttribute("Focus"), DefaultValue(SpeakerPos.None)]
    public SpeakerPos Focus { get; set; } = SpeakerPos.None;

    public override bool Play(DialogueUI dialUI)
    {
        dialUI.PlayLine(this);
        return false;
    }
}