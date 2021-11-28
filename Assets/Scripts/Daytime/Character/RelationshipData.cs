using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

public class RelationshipData
{
    [XmlAttribute("Name")]
    public string RelationName { get; set; }

    [XmlIgnore]
    public Sprite Icon { get { return AssetDB.Instance.Images[_Icon]; } }
    [XmlAttribute("Icon")]
    public string _Icon { get; set; }

    [XmlAttribute("AutoColor"), DefaultValue(true)]
    public bool AutoColor { get; set; }

    [XmlElement("Event")]
    public List<ConditionalDialogue> RelationEvents { get; set; }

    [XmlAttribute("Description")]
    public string Description { get; set; }
}
