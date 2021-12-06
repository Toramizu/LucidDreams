using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ArousalBonus : DialogueElement
{
    [XmlAttribute("Character")]
    public string Character { get; set; }
    [XmlAttribute("Value")]
    public int Value { get; set; }
    [XmlAttribute("isMod")]
    public bool Mod { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        GameManager.Instance.NightPreps.AddArousalBonus(Character, Value, Mod);
        return true;
    }
}
