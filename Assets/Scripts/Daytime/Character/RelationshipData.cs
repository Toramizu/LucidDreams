using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
public class RelationshipData
{
    [XmlAttribute("Name")]
    public string RelationName { get; set; }

    [XmlIgnore]
    public Sprite icon;

    [XmlElement("Event")]
    public List<InteractionDialogue> RelationEvents { get; set; }
}
