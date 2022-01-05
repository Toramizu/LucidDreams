using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueLoop : DialogueElement
{
    [XmlAttribute("ID")]
    public string ID { get; set; }
    [XmlAttribute("GoTo")]
    public bool GoTo { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        if (GoTo)
            dialUI.GoTo(ID);
        else
            dialUI.AddLoop(ID);

        return true;
    }
}
