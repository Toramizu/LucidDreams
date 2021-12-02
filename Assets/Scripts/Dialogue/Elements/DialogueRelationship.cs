using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueRelationship : DialogueElement
{
    [XmlAttribute("Character")]
    public string Character { get; set; }
    [XmlAttribute("Relationship")]
    public int Relationship { get; set; }
    [XmlAttribute("Points")]
    public int Points { get; set; }

    public override bool Play(DialogueUI dialUI)
    {
        Character chara = AssetDB.Instance.Characters[Character];
        chara.AddRelationPoints(Relationship, Points);

        /*if(Points > 0)
            GameManager.Instance.Notify(Character + " => " + chara.Relationships[Relationship].Name + " +" + Points);
        else
            GameManager.Instance.Notify(Character + " => " + chara.Relationships[Relationship].Name + " " + Points);*/
        return true;
    }
}
