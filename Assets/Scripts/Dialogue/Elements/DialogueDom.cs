using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueDom : DialogueElement
{
    [XmlAttribute("Amount")]
    public int Amount { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        Flags.Instance.FlagAdd("Dominance", Amount);

        if (Amount > 0)
            GameManager.Instance.Notify("Dom +" + Amount);
        else
            GameManager.Instance.Notify("Sub +" + (-Amount));

        return true;
    }
}
