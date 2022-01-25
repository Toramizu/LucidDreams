using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueSpeaker : DialogueElement
{
    [XmlAttribute("Speaker")]
    public string Speaker { get; set; }
    [XmlAttribute("Displayed")]
    public string Displayed { get; set; }
    [XmlAttribute("Image")]
    public string ImageID { get; set; }
    [XmlAttribute("Position"), DefaultValue(SpeakerPos.Left)]
    public SpeakerPos SpeakerPosition { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        dialUI.SetSpeaker(this);
        return true;
    }
}

public enum SpeakerPos
{
    None,
    Left,
    Left2,
    Right,
    Right2,
}