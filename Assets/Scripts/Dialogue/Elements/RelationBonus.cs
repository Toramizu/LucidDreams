using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class RelationBonus : DialogueElement
{
    [XmlAttribute("Character")]
    public string Character { get; set; }
    [XmlAttribute("Relationship")]
    public int Relationship { get; set; }
    [XmlAttribute("Value")]
    public int Value { get; set; }
    [XmlAttribute("isMod")]
    public bool Mod { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        GameManager.Instance.NightPreps.AddRelationBonus(Character, Relationship, Value, Mod);
        return true;
    }
}
