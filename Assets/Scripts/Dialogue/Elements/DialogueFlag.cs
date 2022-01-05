using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueFlag : DialogueElement
{
    [XmlAttribute("ID")]
    public string ID { get; set; }
    [XmlAttribute("Value")]
    public int Value { get; set; }
    [XmlAttribute("Set")]
    public bool Set { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        if (Set)
            GameManager.Instance.Flags.SetFlag(ID, Value);
        else
            GameManager.Instance.Flags.FlagAdd(ID, Value);

        if (Variables.debugMode)
            GameManager.Instance.Notify(ID + " => " + GameManager.Instance.Flags.GetFlag(ID));

        return true;
    }
}
