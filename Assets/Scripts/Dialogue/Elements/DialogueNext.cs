using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueNext : DialogueElement
{
    [XmlAttribute("ID")]
    public string ID { get; set; }
    [XmlAttribute("Keep")]
    public bool Keep { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        DialogueData dial = AssetDB.Instance.Dialogues[ID];

        if (dial == null)
            GameManager.Instance.NotifyError("No dialogue found for ID : " + ID);
        else if (Keep)
            dialUI.AddInFront(dial.Elements);
        else
            dialUI.QuickOpen(dial);

        return true;
    }
}
