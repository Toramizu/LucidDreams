using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueInput : DialogueElement
{
    [XmlText]
    public string Text { get; set; }
    [XmlAttribute("ID")]
    public string ID { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        dialUI.EnterInput(Text, ID);
        return false;
    }
}
