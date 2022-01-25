using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueRelationship : DialogueElement
{
    [XmlAttribute("Character")]
    public string Character { get; set; }
    [XmlAttribute("Relationship")]
    public RelationType Relationship { get; set; }
    [XmlAttribute("Points")]
    public int Points { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        Character chara = AssetDB.Instance.Characters[Character];
        chara.AddRelationPoints(Relationship, Points, false);

        return true;
    }
}
