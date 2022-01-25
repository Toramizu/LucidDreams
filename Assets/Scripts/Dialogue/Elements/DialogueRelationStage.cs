using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueRelationStage : DialogueElement
{
    [XmlAttribute("Character")]
    public string Character { get; set; }
    [XmlAttribute("Relationship")]
    public RelationType Relationship { get; set; }
    [XmlAttribute("Level")]
    public int Level { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        Character chara = AssetDB.Instance.Characters[Character];
        chara.IncreaseRelationship(Relationship, Level);
        return true;
    }
}
