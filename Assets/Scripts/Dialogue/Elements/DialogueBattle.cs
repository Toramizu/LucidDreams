using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueBattle : DialogueElement
{
    [XmlIgnore]
    public SuccubusData Opponent
    {
        get { return AssetDB.Instance.Succubi[_Opponent]; }
    }
    [XmlAttribute("Opponent")]
    public string _Opponent { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        GameManager.Instance.StartBattle(Opponent);
        return true;
    }
}
