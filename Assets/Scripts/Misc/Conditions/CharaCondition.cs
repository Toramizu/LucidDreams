using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CharaCondition : Condition
{
    [XmlAttribute("Chara")]
    string charaID;

    public override bool Check
    {
        get { return false; }
    }
}
