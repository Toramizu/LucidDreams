using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

public class RelationshipData
{
    [XmlAttribute("Name")]
    public string RelationName { get; set; }

    [XmlAttribute("Type")]
    public RelationType Type { get; set; }

    [XmlIgnore]
    public Sprite Icon { get { return AssetDB.Instance.Images[_Icon]; } }
    [XmlAttribute("Icon")]
    public string _Icon { get; set; }

    [XmlElement("Color")]
    public ColorXml Color { get; set; }

    [XmlElement("Event")]
    public List<ConditionalDialogue> RelationEvents { get; set; }

    [XmlAttribute("Description")]
    public string Description { get; set; }
}

public enum RelationType
{
    Friendship,
    Love,
    Loss,
    Other,
}