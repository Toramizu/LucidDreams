using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueBattle : DialogueElement
{
    [XmlIgnore]
    public SuccubusData Player
    {
        get { return AssetDB.Instance.Succubi[_Player]; }
    }
    [XmlAttribute("Player")]
    public string _Player { get; set; }
    [XmlIgnore]
    public SuccubusData Opponent
    {
        get { return AssetDB.Instance.Succubi[_Opponent]; }
    }
    [XmlAttribute("Opponent")]
    public string _Opponent { get; set; }
    [XmlAttribute("WinEvent")]
    public string _WinEvent { get; set; }
    [XmlAttribute("LossEvent")]
    public string _LossEvent { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        if(_Player == null)
            GameManager.Instance.StartBattle(Opponent, OnWin, OnLoss);
        else
            GameManager.Instance.StartSingleBattle(Player, Opponent, OnWin, OnLoss);
        return true;
    }

    public void OnWin()
    {
        if(_WinEvent != null)
            GameManager.Instance.StartDialogue(AssetDB.Instance.Dialogues[_WinEvent], null);
    }

    public void OnLoss()
    {
        if (_LossEvent != null)
            GameManager.Instance.StartDialogue(AssetDB.Instance.Dialogues[_LossEvent], null);
    }
}
