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
            Flags.Instance.SetFlag(ID, Value);
        else
            Flags.Instance.FlagAdd(ID, Value);

        if (Variables.debugMode)
            GameManager.Instance.Notify(ID + " => " + Flags.Instance.GetFlag(ID));

        return true;
    }
}
