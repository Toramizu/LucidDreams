using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueLines : DialogueElement
{
    [XmlElement("Line")]
    [SerializeField] public List<DialogueLine> lines;
    [XmlIgnore]
    public List<DialogueLine> Lines { get { return lines; } }

    public override bool Play(DialogueUI dialUI)
    {
        dialUI.Play(lines);
        return false;
    }
}