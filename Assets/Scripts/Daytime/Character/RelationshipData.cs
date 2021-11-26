using System.Collections;
using System.Collections.Generic;
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

    [XmlElement("Event")]
    public List<ConditionalDialogue> RelationEvents { get; set; }
}
