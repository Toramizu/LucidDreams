using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueBattle : DialogueElement
{
    [XmlIgnore]
    [SerializeField] SuccubusData opponent;
    [XmlIgnore]
    public SuccubusData Opponent
    {
        get {
            if(opponent == null)
                return GameManager.Instance.Assets.Succubi[_Opponent];
            return opponent;
        }
        set {
            _Opponent = value.Name;
        }
    }
    [XmlAttribute("Opponent")]
    public string _Opponent { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        GameManager.Instance.StartBattle(Opponent);
        return true;
    }
}
