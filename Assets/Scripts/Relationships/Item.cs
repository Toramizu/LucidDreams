using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Item : XmlAsset
{
    [XmlAttribute("ID")]
    public string ID { get; set; }
    [XmlText]
    public string Description { get; set; }

    [XmlIgnore]
    public Sprite Icon
    {
        get
        {
            return AssetDB.Instance.Images[_Icon];
        }
    }
    [XmlAttribute("Icon")]
    public string _Icon { get; set; }

    [XmlElement("OnUse")]
    public DialogueData OnUse { get; set; }
}
