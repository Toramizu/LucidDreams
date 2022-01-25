using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

public class StringCondition : Condition
{
    [XmlAttribute("Flag")]
    string flag;
    [XmlAttribute("Value")]
    string value;
    [XmlAttribute("Equal"), DefaultValue(true)]
    bool equal;

    public override bool Check
    {
        get
        {
            return (Flags.Instance.GetString(flag) == value) == equal;
        }
    }
}
